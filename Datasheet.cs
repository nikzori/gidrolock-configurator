using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
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
        static bool isAwaitingResponse = false;
        static bool responseReceived = false;


    bool isValveClosed = false;
        bool alarmStatus = false;
        bool cleaningStatus = false;

        List<WiredSensor> wiredSensors = new List<WiredSensor>();
        List<WirelessSensor> wirelessSensors;
        public static string firmwarePath;

        static int timeout = 1000;
        Stopwatch stopwatch = new Stopwatch();

        Thread fileThread = new Thread((ThreadStart)delegate
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
                firmwarePath = ofd.FileName;
        });

        public Datasheet(byte modbusID, Device device) : base()
        {
            InitializeComponent();
            firmwareProgressBar.Minimum = 0;
            firmwareProgressBar.Maximum = 100;
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

            Modbus.ResponseReceived += (sndr, msg) => { responseReceived = true; latestMessage = msg; isAwaitingResponse = false; };

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
                bool res = await PollEntry(device.firmware);
                Console.WriteLine("Polling for alarm status, poll success: " + res);
                if (res)
                {
                    labelFirmware.Text = App.ByteArrayToUnicode(latestMessage.Data);
                }

                if (device.hasBattery)
                {
                    res = await PollEntry(device.batteryCharge);
                    if (res)
                    {
                        labelBattery.Text = latestMessage.Data.Last().ToString();
                    }
                }

                res = await PollEntry(device.valveStatus);
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

                res = await PollEntry(device.alarmStatus);
                Console.WriteLine("Polling for alarm status, poll success: " + res);
                if (res)
                {
                    Console.WriteLine("Alarm data: " + Modbus.ByteArrayToString(latestMessage.Data));
                    Console.WriteLine("Alarm data.last: " + latestMessage.Data.Last().ToString());
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
                            buttonCleaning.Text = "Выключить";
                            labelCleaning.Text = "вкл";
                        }
                        else
                        {
                            cleaningStatus = false;
                            buttonCleaning.Text = "Включить";
                            labelCleaning.Text = "выкл";
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
            isAwaitingResponse = true;
            Modbus.ReadRegAsync(modbusID, (FunctionCode)entry.registerType, entry.address, entry.length);

            stopwatch.Restart();

            while (isAwaitingResponse)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    Console.WriteLine("Response timed out.");
                    break;
                }
            }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
                res = true;

            Console.WriteLine("Poll attempt finished");
            return res;
        }

        private async void buttonSetID_Click(object sender, EventArgs e)
        {
            byte newID = (byte)nudModbusID.Value; // should prevent assigning wrong ID if UpDown is fiddled with in the middle of request 
            isAwaitingResponse = true;
            Modbus.WriteSingleAsync(FunctionCode.WriteRegister, modbusID, 128, newID);

            stopwatch.Restart();
            while (isAwaitingResponse)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    Console.WriteLine("Response timed out.");
                    break;
                }
            }

            if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
                modbusID = newID;
        }

        private async void buttonValve_Click(object sender, EventArgs e)
        {
            ushort value = isValveClosed ? (ushort)0 : (ushort)0xFF00;
            isAwaitingResponse = true;
            Modbus.WriteSingleAsync(FunctionCode.WriteCoil, modbusID, device.valveStatus.address, value);

            stopwatch.Restart();
            while (isAwaitingResponse)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    Console.WriteLine("Response timed out.");
                    break;
                }
            }

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
            isAwaitingResponse = true;
            Modbus.WriteSingleAsync(FunctionCode.WriteCoil, modbusID, device.alarmStatus.address, value);

            stopwatch.Restart();
            while (isAwaitingResponse)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    Console.WriteLine("Response timed out.");
                    break;
                }
            }

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
            isAwaitingResponse = true;
            Modbus.WriteSingleAsync(FunctionCode.WriteCoil, modbusID, device.cleaningMode.address, value);

            stopwatch.Restart();
            while (isAwaitingResponse)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    Console.WriteLine("Response timed out.");
                    break;
                }
            }

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
                isAwaitingResponse = true;
                Modbus.WriteSingleAsync(FunctionCode.WriteRegister, modbusID, device.baudRate.address, newSpeed);

                stopwatch.Restart();
                while (isAwaitingResponse)
                {
                    if (stopwatch.ElapsedMilliseconds > 1000)
                    {
                        Console.WriteLine("Response timed out.");
                        break;
                    }
                }

                if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
                {
                    port.Close();
                    port.BaudRate = newSpeed;
                    port.Open();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BrowseFirmware_Click(object sender, EventArgs e)
        {
            try
            {
                fileThread.SetApartmentState(ApartmentState.STA);
                fileThread.Start();
                while (!fileThread.IsAlive) { Thread.Sleep(1); }
                Thread.Sleep(1);
                fileThread.Join();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

            firmwarePathLabel.Invoke(new MethodInvoker(delegate { firmwarePathLabel.Text = firmwarePath; }));
        }

        private async void WriteFirmware_Click(object sender, EventArgs e)
        {
            if (firmwarePath is null || firmwarePath.Length == 0)
            {
                MessageBox.Show("Выберите файл прошивки.");
                return;
            }
            int cntr = 0;

            FileStream fileStream = File.OpenRead(firmwarePath);
            long bytesLeft = fileStream.Length;
            long bytesTotal = fileStream.Length;
            int count = 64;
            byte[] buffer = new byte[count];
            byte[] bdma;
            short _flashAddr = 0;
            byte[] flashAddr = new byte[2];
            byte[] CRC;
            List<byte> message;
            bool firstMessageSent = false;
            long bytesWritten = 0;
            await Task.Run(() =>
            {
                while (bytesLeft > 0)
                {
                    if (firstMessageSent && port.BaudRate != 9600) // after first message the device is sent into recovery mode which only supports 9600 bps
                    {
                        port.Close();
                        port.BaudRate = 9600;
                        port.Open();
                    }

                    count = bytesLeft > 64 ? 64 : (int)bytesLeft;
                    buffer = new byte[count];
                    fileStream.Read(buffer, 0, count);


                    bdma = new byte[2];
                    bdma[0] = (byte)((bytesLeft & 0xFF_00) >> 8);
                    bdma[1] = (byte)(bytesLeft & 0x00_FF);

                    flashAddr[0] = (byte)((_flashAddr & 0xFF_00) >> 8);
                    flashAddr[1] = (byte)(_flashAddr & 0x00_FF);

                    message = new List<byte>();
                    message.Add(modbusID);  // device ID
                    message.Add(0x10);      // function code
                    message.Add(0xFF);      // register address
                    message.Add(0xFF);      // register address
                    message.Add(0x00);      // regCnt (?)
                    message.Add(0x21);      // regCnt (?)
                    message.Add(0x42);      // data bytecount

                    message.Add(flashAddr[0]);
                    message.Add(flashAddr[1]);

                    try
                    {
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            message.Add(buffer[i]);
                        }
                        message.Add(0x00);
                        message.Add(0x00);
                        CRC = new byte[2];
                        Modbus.GetCRC(message.ToArray(), ref CRC);
                        message[message.Count - 2] = CRC[0];
                        message[message.Count - 1] = CRC[1];

                        responseReceived = false;


                        while (true)
                        {
                            if (cntr > 3)
                            {
                                Console.WriteLine("Response timed out 4 times in a row, aborting. Check connection.");
                                return;
                            }
                            isAwaitingResponse = true;
                            Console.WriteLine("Outgoing firmware message: " + Modbus.ByteArrayToString(message.ToArray()));
                            port.Write(message.ToArray(), 0, message.Count);
                            stopwatch.Restart();

                            while (isAwaitingResponse)
                            {
                                if (stopwatch.ElapsedMilliseconds > 1000)
                                {
                                    Console.WriteLine("Response timed out.");
                                    cntr++;
                                    break;
                                }
                            }

                            if (responseReceived)
                            {
                                if (latestMessage.Status == ModbusStatus.Error)
                                    Console.WriteLine("Response received: Error!");
                                else
                                {
                                    Console.WriteLine("Response received: all good;");
                                    break;
                                }
                            }
                        }
                        cntr = 0;
                        bytesLeft -= count;
                        bytesWritten += count;
                        firmwareProgressBar.Invoke((MethodInvoker)delegate { firmwareProgressBar.Increment((int)(bytesWritten / bytesTotal) * 100); });

                        _flashAddr += (short)count;
                        if (port.BaudRate != 9600)
                            firstMessageSent = true;

                        if (bytesLeft <= 0)
                            Console.WriteLine("Reached the end of firmware file.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Firmware writing error");
                        return;
                    }

                }

                /* Final Message */
                message = new List<byte>();
                message.Add(modbusID);  // device ID
                message.Add(0x10);      // function code
                message.Add(0xFF);      // register address
                message.Add(0xFF);      // register address
                message.Add(0x00);      // regCnt (?)
                message.Add(0x21);      // regCnt (?)
                message.Add(0x00);      // data bytecount
                message.Add(0x00);      // CRC
                message.Add(0x00);      // CRC
                CRC = new byte[2];
                Modbus.GetCRC(message.ToArray(), ref CRC);
                message[message.Count - 2] = CRC[0];
                message[message.Count - 1] = CRC[1];
                while (true)
                {
                    isAwaitingResponse = true;
                    Console.WriteLine("Outgoing firmware message: " + Modbus.ByteArrayToString(message.ToArray()));
                    port.Write(message.ToArray(), 0, message.Count);
                    stopwatch.Restart();

                    while (isAwaitingResponse)
                    {
                        if (stopwatch.ElapsedMilliseconds > 1000)
                        {
                            Console.WriteLine("Response timed out.");
                            break;
                        }
                    }

                    if (responseReceived)
                    {
                        if (latestMessage.Status == ModbusStatus.Error)
                            Console.WriteLine("Response received: Error!");
                        else
                        {
                            Console.WriteLine("Response received: all good;");
                            break;
                        }
                    }
                }
            });

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