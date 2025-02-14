using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gidrolock_Modbus_Scanner
{
    public partial class Datasheet : Form
    {
        byte modbusID;
        Device device;

        ModbusResponseEventArgs latestMessage;

        SerialPort port = Modbus.port;
        bool isPolling = false;
        bool isAwaitingResponse = false;


        bool isValveClosed = false;
        bool alarmStatus = false;
        bool cleaningStatus = false;

        List<WiredSensor> wiredSensors = new List<WiredSensor>();
        List<WirelessSensor> wirelessSensors;
        


        public Datasheet(byte modbusID, Device device) : base()
        {
            InitializeComponent();

            nudModbusID.Minimum = 1;
            nudModbusID.Maximum = 246;
            nudModbusID.Value = modbusID;
            this.modbusID = modbusID;
            this.device = device;

            cBoxSpeed.Items.Add("1200");
            cBoxSpeed.Items.Add("2400");
            cBoxSpeed.Items.Add("4800");
            cBoxSpeed.Items.Add("9600");
            cBoxSpeed.Items.Add("14400");
            cBoxSpeed.Items.Add("19200");
            cBoxSpeed.Items.Add("38400");
            cBoxSpeed.Items.Add("57600");
            cBoxSpeed.Items.Add("115200");
            cBoxSpeed.Text = port.BaudRate.ToString();

            labelModel.Text = device.name;
            labelFirmware.Text = "v???";

            if (!device.hasCleaningMode)
            {
                labelCleaning.Text = "Недоступна.";
                buttonCleaning.Enabled = false;
            }
            if (!device.hasBattery)
                labelBattery.Text = "Нет";
            else labelBattery.Text = "???%";

            Modbus.ResponseReceived += (sndr, msg) => { isAwaitingResponse = false; latestMessage = msg; };

            for (int i = 0; i < device.wiredSensors; i++)
            {
                WiredSensor ws = new WiredSensor(i) { Width = 495, Height = 24 };
                sensorPanel.Controls.Add(ws);
                ws.Visible = true;
                
            }
            if (device.hasScenarioSensor)
            {
                ScenarioSensor scenSen = new ScenarioSensor() { Width = 495, Height = 24 };
                sensorPanel.Controls.Add(scenSen);
                scenSen.Visible = true;
            }
            if (device.wiredSensors < device.sensorAlarm.length)
            {
                wirelessSensors = new List<WirelessSensor>();
                int wsrIndex = device.sensorAlarm.length - device.wiredSensors - (device.hasScenarioSensor ? 1 : 0);
                for (int i = 0; i < wsrIndex; i++)
                {
                    WirelessSensor wsr = new WirelessSensor(i) { Width = 495, Height = 24 };
                    sensorPanel.Controls.Add(wsr);
                    wsr.Visible = true;
                }
            }

            for (int i = 0; i < sensorPanel.Controls.Count; i++)
                sensorPanel.Controls[i].BackColor = i % 2 == 0 ? Color.White : Color.LightGray;


            sensorPanel.Update();
        }

        private async void buttonPoll_Click(object sender, EventArgs e)
        {
            // hardcoded for now, probably easier to keep it like this in the future
            try
            {
                bool res = await PollEntry(device.valveStatus); // for some reason main thread doesn't go
                Console.WriteLine("Polling for valve status, poll success: " + res);
                if (res)
                {
                    if (latestMessage.Data.Last() > 0)
                    {
                        isValveClosed = true;
                        labelValve.Text = "Закрыт";
                        buttonValve.Text = "Открыть";
                    }
                    else
                    {
                        isValveClosed = false;
                        labelValve.Text = "Открыт";
                        buttonValve.Text = "Закрыть";
                    }
                }
                res = await PollEntry(device.firmware);
                Console.WriteLine("Polling for alarm status, poll success: " + res);
                if (res)
                {
                    labelFirmware.Text = App.ByteArrayToUnicode(latestMessage.Data);
                }
                res = await PollEntry(device.alarmStatus);
                Console.WriteLine("Polling for alarm status, poll success: " + res);
                if (res)
                {
                    if (latestMessage.Data.Last() > 0)
                    {
                        alarmStatus = true;
                        buttonAlarm.Text = "Выключить";
                        labelAlarm.Text = "Протечка!";
                    }
                    else
                    {
                        alarmStatus = false;
                        buttonAlarm.Text = "Авария";
                        labelAlarm.Text = "нет";
                    }
                }

                if (device.hasCleaningMode)
                {
                    res = await PollEntry(device.cleaningMode);
                    if (res)
                    {
                        if (latestMessage.Data.Last() > 0)
                        {
                            cleaningStatus = true;
                            buttonAlarm.Text = "Выключить";
                            labelAlarm.Text = "вкл";
                        }
                        else
                        {
                            cleaningStatus = false;
                            buttonAlarm.Text = "Включить";
                            labelAlarm.Text = "выкл";
                        }
                    }
                }

                if (device.hasBattery)
                {
                    res = await PollEntry(device.batteryCharge);
                    if (res)
                    {
                        labelBattery.Text = latestMessage.Data.Last().ToString();
                    }
                }
                if (device.hasCleaningMode)
                {
                    res = await PollEntry(device.cleaningMode);
                    if (res)
                    {
                        if (latestMessage.Data.Last() > 0)
                        {
                            cleaningStatus = true;
                            buttonAlarm.Text = "Выключить";
                            labelAlarm.Text = "вкл";
                        }
                        else
                        {
                            cleaningStatus = false;
                            buttonAlarm.Text = "Включить";
                            labelAlarm.Text = "выкл";
                        }
                    }
                }

                res = await PollEntry(device.sensorAlarm);
                if (res)
                {
                    BitArray bArray = new BitArray(latestMessage.Data);
                    bool[] bools = new bool[bArray.Length];
                    bArray.CopyTo(bools, 0);
                    for (int i = 0; i < sensorPanel.Controls.Count; i++)
                    {
                        Sensor snsr = sensorPanel.Controls[i] as Sensor;
                        snsr.labelLeak.Text = bools[i] ? "Протечка!" : "нет";
                    }
                }

                res = await PollEntry(device.radioStatus);
                if (res)
                {
                    List<byte> values = new List<byte>(latestMessage.Data.Length / 2);
                    for (int i = 1; i < latestMessage.Data.Length; i += 2)
                        values.Add(latestMessage.Data[i]);
                    int add = device.wiredSensors + (device.hasScenarioSensor ? 1 : 0);
                    for (int i = 0; i < sensorPanel.Controls.Count - add; i++)
                    {

                        WirelessSensor snsr = sensorPanel.Controls[i + add] as WirelessSensor;
                        string txt = "нет";
                        switch (values[i])
                        {
                            case 1:
                                txt = "норма";
                                break;
                            case 2:
                                txt = "протечка";
                                break;
                            case 3:
                                txt = "разряжен";
                                break;
                            case 4:
                                txt = "потеря";
                                break;
                        }
                        snsr.labelStatus.Text = txt;
                    }
                }

            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        async Task<bool> PollEntry(Entry entry)
        {
            bool res = false;
            Modbus.ReadRegAsync(modbusID, (FunctionCode)entry.registerType, entry.address, entry.length);
            isAwaitingResponse = true;
            Task.Delay(2000).ContinueWith(_ =>
            {
                if (isAwaitingResponse)
                {
                    MessageBox.Show("Превышено время ожидания ответа от устройства.");
                    isAwaitingResponse = false;
                }
            });
            while (isAwaitingResponse) { continue; }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
                res = true;

            Console.WriteLine("Poll attempt finished");
            return res;
        }

        private async void buttonSetID_Click(object sender, EventArgs e)
        {
            byte newID = (byte)nudModbusID.Value; // should prevent assigning wrong ID if UpDown is fiddled with in the middle of request 
            Modbus.WriteSingleAsync(FunctionCode.WriteRegister, modbusID, 128, newID);
            await Task.Delay(2000).ContinueWith(_ =>
            {
                if (isAwaitingResponse)
                {
                    MessageBox.Show("Превышено время ожидания ответа от устройства.");
                    isAwaitingResponse = false;
                }
                return false;
            });
            while (isAwaitingResponse) { continue; }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
                modbusID = newID;
        }

        private async void buttonValve_Click(object sender, EventArgs e)
        {
            ushort value = isValveClosed ? (ushort)0: (ushort)0xFF00;
            Modbus.WriteSingleAsync(FunctionCode.WriteCoil, modbusID, device.valveStatus.address, value);

            await Task.Delay(2000).ContinueWith(_ =>
            {
                if (isAwaitingResponse)
                {
                    MessageBox.Show("Превышено время ожидания ответа от устройства.");
                    isAwaitingResponse = false;
                }
                return false;
            });
            while (isAwaitingResponse) { continue; }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
            {
                isValveClosed = !isValveClosed;
                labelValve.Text = isValveClosed ? "Закрыт" : "Открыт";
                buttonValve.Text = isValveClosed ? "Открыть" : "Закрыть";
            }
        }

        private async void buttonAlarm_Click(object sender, EventArgs e)
        {
            ushort value = alarmStatus ? (ushort)0 : (ushort)0xFF00;
            Modbus.WriteSingleAsync(FunctionCode.WriteCoil, modbusID, device.alarmStatus.address, value);

            await Task.Delay(2000).ContinueWith(_ =>
            {
                if (isAwaitingResponse)
                {
                    MessageBox.Show("Превышено время ожидания ответа от устройства.");
                    isAwaitingResponse = false;
                }
                return false;
            });
            while (isAwaitingResponse) { continue; }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
            {
                alarmStatus = !alarmStatus;
                labelAlarm.Text = alarmStatus ? "Протечка!" : "Нет";
                buttonAlarm.Text = alarmStatus ? "Выключить" : "Авария";
            }
        }

        private async void buttonCleaning_Click(object sender, EventArgs e)
        {
            ushort value = cleaningStatus ? (ushort)0 : (ushort)0xFF00;
            Modbus.WriteSingleAsync(FunctionCode.WriteCoil, modbusID, device.cleaningMode.address, value);

            await Task.Delay(2000).ContinueWith(_ =>
            {
                if (isAwaitingResponse)
                {
                    MessageBox.Show("Превышено время ожидания ответа от устройства.");
                    isAwaitingResponse = false;
                }
                return false;
            });
            while (isAwaitingResponse) { continue; }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
            {
                cleaningStatus = !cleaningStatus;
                labelCleaning.Text = cleaningStatus ? "вкл" : "выкл";
                buttonCleaning.Text = cleaningStatus ? "Выключить" : "Включить";
            }
        }

        private async void buttonSetSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                string str = cBoxSpeed.Items[cBoxSpeed.SelectedIndex].ToString();
                str = str.Substring(0, str.Length - 2); //clip off two zeroes at the end
                ushort newSpeed = (ushort)Int16.Parse(str);
                //Console.WriteLine("Baudrate: " + newSpeed);

                // send speed value to device
                // await for response
                Modbus.WriteSingleAsync(FunctionCode.WriteRegister, modbusID, device.baudRate.address, newSpeed);
                await Task.Delay(2000).ContinueWith(_ =>
                {
                    if (isAwaitingResponse)
                    {
                        MessageBox.Show("Превышено время ожидания ответа от устройства.");
                        isAwaitingResponse = false;
                    }
                    return false;
                });
                while (isAwaitingResponse) { continue; }
                if (latestMessage.Status != ModbusStatus.Error)
                {
                    port.Close();
                    port.BaudRate = newSpeed;
                    port.Open();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
    public class Sensor : FlowLayoutPanel
    {
        public Label labelName = new Label() { Width = 60, Height = 24 };
        public Label labelLeakFluff = new Label() { Width = 60, Height = 24 };
        public Label labelLeak = new Label() { Width = 50, Height = 24 };
    }
    public class WiredSensor : Sensor
    {

        public Label labelBreakFluff = new Label() { Width = 45, Height = 24 };
        public Label labelBreak = new Label() { Width = 55, Height = 24 }; // обрыв линии для WSP+

        //public Label labelWSPPlusFluff = new Label() { Width = 45, Height = 24 };
        //public CheckBox wspPlusCheckbox = new CheckBox() { Width = 20, Height = 14 };
        public WiredSensor(int count)
        {
            this.Margin = Padding.Empty;
            this.Padding = new Padding(0, 5, 0, 0);
            this.WrapContents = false;
            this.BackColor = Color.White;
            this.Height = 15;
            this.FlowDirection = FlowDirection.LeftToRight;
            this.Controls.Add(labelName);

            this.Controls.Add(labelBreakFluff);
            this.Controls.Add(labelBreak);

            this.Controls.Add(labelLeakFluff);
            this.Controls.Add(labelLeak);

            //this.Controls.Add(labelWSPPlusFluff);
            //this.Controls.Add(wspPlusCheckbox);

            labelName.Text = "WSP " + (count + 1);

            labelLeakFluff.Text = "Протечка:";
            labelLeak.Text = "неизвестно";

            labelBreakFluff.Text = "Обрыв:";
            labelBreak.Text = "неизвестно";

            //labelWSPPlusFluff.Text = "WSP+:";
            //wspPlusCheckbox.Margin = Padding.Empty;
        }
    }

    public class WirelessSensor : Sensor
    {

        public Label labelStatusFluff = new Label() { Width = 45, Height = 24 };
        public Label labelStatus = new Label() { Width = 55, Height = 24 };

        public WirelessSensor(int count)
        {
            this.Margin = Padding.Empty;
            this.Padding = new Padding(0, 5, 0, 0);
            this.BackColor = Color.White;
            this.FlowDirection = FlowDirection.LeftToRight;
            this.WrapContents = false;

            this.Controls.Add(labelName);
            this.Controls.Add(labelStatusFluff);
            this.Controls.Add(labelStatus);
            this.Controls.Add(labelLeakFluff);
            this.Controls.Add(labelLeak);

            labelName.Text = "WSR " + (count + 1);

            labelLeakFluff.Text = "Протечка:";
            labelLeak.Text = "неизвестно";

            labelStatusFluff.Text = "Статус:";
            labelStatus.Text = "неизвестно";
        }
    }

    public class ScenarioSensor : Sensor
    {
        public ScenarioSensor()
        {
            labelName.Width = 172;
            this.Margin = Padding.Empty;
            this.Padding = new Padding(0, 5, 0, 0);
            this.FlowDirection = FlowDirection.LeftToRight;
            this.WrapContents = false;

            this.Controls.Add(labelName);
            this.Controls.Add(labelLeakFluff);
            this.Controls.Add(labelLeak);

            labelName.Text = "Сценарный датчик";

            labelLeakFluff.Text = "Протечка:";
            labelLeak.Text = "неизвестно";
        }
    }
}