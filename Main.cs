using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Newtonsoft.Json;

namespace Gidrolock_Modbus_Scanner
{
    public partial class App : Form
    {

        public bool isAwaitingResponse = false;
        public short[] res = new short[12];
        SerialPort port = Modbus.port;
        public int expectedLength = 0;

        public byte[] latestMessage;
        public Dictionary<string, string> models = new Dictionary<string, string>();

        byte[] message = null;
        byte[] data = null;
        

    DateTime dateTime;

        Datasheet datasheet;
        #region Initialization
        public App()
        {
            InitializeComponent();
            Modbus.Init();
            Modbus.ResponseReceived += OnResponseReceived;

            this.UpDown_ModbusID.Value = 0;
            TextBox_Log.Text = "Приложение готово к работе.";

            cBoxDevice.Items.Add("Standard");
            cBoxDevice.Items.Add("Inteli");
            cBoxDevice.Items.Add("Premium Plus");
            cBoxDevice.Items.Add("Premium");
            cBoxDevice.SelectedIndex = 0;

            checkboxID.Checked = false;
            UpDown_ModbusID.Enabled = false;
            UpDown_ModbusID.Value = 30;
            UpDown_ModbusID.Minimum = 1;
            UpDown_ModbusID.Maximum = 247;

            models.Add("Standard", "STW485");
            models.Add("Inteli", "INTELI");
            models.Add("Premuim Plus", "BUP485");
            models.Add("Premium", "BUP485");

            /* - Version Check - */
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Console.WriteLine("Version: " + version);

            Modbus.ResponseReceived += (sndr, msg) =>
            {
                message = msg.Message;
                data = msg.Data;
            };
        }

        void App_FormClosed(object sender, FormClosedEventArgs e)
        {
            port.Close();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            CBox_Ports.Items.AddRange(SerialPort.GetPortNames());
            if (CBox_Ports.Items.Count > 0)
                CBox_Ports.SelectedIndex = 0;
        }
        #endregion

        private async void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (CBox_Ports.SelectedItem.ToString() == "COM1")
            {
                DialogResult res = MessageBox.Show("Выбран серийный порт COM1, который обычно является портом PS/2 или RS-232, не подключенным к Modbus устройству. Продолжить?", "Внимание", MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                    return;
            }
            if (UpDown_ModbusID.Value == 0)
            {
                DialogResult res = MessageBox.Show("Указан Modbus ID 0 — глобальное вещание. Устройства не смогут отвечать на сообщения. Продолжить?", "Внимание", MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                    return;
            }
            try
            {

                /* - Port Setup - */
                if (port.IsOpen)
                    port.Close();

                port.Handshake = Handshake.None;
                port.PortName = CBox_Ports.Text;
                port.BaudRate = 9600;
                port.Parity = Parity.None;
                port.DataBits = 8;
                port.StopBits = StopBits.One;

                port.ReadTimeout = 3000;
                port.WriteTimeout = 3000;
                port.ReadBufferSize = 8192;

                port.Open();

                // send read request for 6 registers starting from 200
                // if we've got error or a timeout, show appropriate errors
                // else parse response to unicode and go through every .json
                // if matching model is found, instantiate device window

                // setup event listener


                // send message
                AddLog("Проверка модели устройства.");

                if (await PollModel())
                {
                    Console.WriteLine("true");
                    // confirm the model
                    string response = ByteArrayToUnicode(data);
                    if (response != models[cBoxDevice.SelectedItem.ToString()])
                    {
                        // response doesn't match expected model
                        AddLog("Ответ устройства не соответствует выбранной модели. Поиск подходящей модели.");
                        // check whether it matches anything else, offer to open that model if it does
                        string match = "";
                        string model = "";
                        int index = 0;
                        foreach (string key in models.Keys)
                        {
                            if (models[key] == response)
                            {
                                match = models[key];
                                model = key;
                                break;
                            }
                            index++;
                        }


                        if (match == "") // if no matches found
                            MessageBox.Show("Подключенное устройство не соответствует ни одной из поддерживаемых моделей.");

                        else // match found, offer to switch to that model instead
                        {
                            DialogResult result = MessageBox.Show(("Подключенное устройство соответствует модели " + model + ". Нажмите ОК, чтобы открыть настройки для модели " + model), "Внимание", MessageBoxButtons.OKCancel);
                            if (result == DialogResult.OK)
                            {
                                // instantiate panel
                                AddLog("Открываю панель конфигурации.");

                                Device device = GetDevice((DeviceType)index);
                                StartDatasheet(device);
                            }
                        }
                    }
                    else
                    {
                        // model is correct, instantiate config panel
                        int index = cBoxDevice.SelectedIndex;
                        AddLog("Устройство соответстует модели, открываю панель конфигурации.");
                        Device device = GetDevice((DeviceType)index);
                        StartDatasheet(device);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        async Task<bool> PollModel()
        {
            var send = Modbus.ReadRegAsync((byte)UpDown_ModbusID.Value, FunctionCode.ReadInput, 200, 6);
            isAwaitingResponse = true;

            Task<bool> delay = Task.WhenAny(Task.Delay(2000), Task.Run(() => { while (isAwaitingResponse) { } return true; })).ContinueWith((t) =>
            {
                if (isAwaitingResponse)
                {
                    Console.WriteLine("Response timed out.");
                    isAwaitingResponse = false;
                }
                return false;
            });

            return await delay;
        }
        void StartDatasheet(Device device)
        {
            datasheet = new Datasheet((byte)UpDown_ModbusID.Value, device);
            datasheet.Show();
        }


        void CBox_Ports_Click(object sender, EventArgs e)
        {
            CBox_Ports.Items.Clear();
            CBox_Ports.Items.AddRange(SerialPort.GetPortNames());
        }

        void OnResponseReceived(object sender, ModbusResponseEventArgs e)
        {
            isAwaitingResponse = false;
            AddLog("Получен ответ: " + Modbus.ByteArrayToString(e.Message));
            switch (e.Status)
            {
                case (ModbusStatus.ReadSuccess):
                    string values = "";
                    if (e.Message[1] <= 0x02)
                    {
                        for (int i = 0; i < e.Data.Length; i++)
                        {
                            values += Convert.ToString(e.Data[i], 2).PadLeft(8, '0') + " ";
                        }
                        AddLog("Bin: " + values);
                    }
                    if (e.Message[1] == 0x03 || e.Message[1] == 0x04)
                    {
                        for (int i = 0; i < e.Data.Length; i += 2)
                        {
                            values += ((e.Data[i] << 8) + e.Data[i + 1]).ToString() + "; ";
                        }
                        AddLog("Dec: " + values);
                        AddLog("Unicode: " + ByteArrayToUnicode(e.Data));
                    }
                    break;
                case (ModbusStatus.WriteSuccess):
                    AddLog("Write success;");
                    break;
                case (ModbusStatus.Error):
                    string errorDesc;
                    switch (e.Message[2])
                    {
                        case (0x01):
                            errorDesc = "01 - Illegal Function";
                            break;
                        case (0x02):
                            errorDesc = "02 - Illegal Data Address";
                            break;
                        case (0x03):
                            errorDesc = "03 - Illegal Data Value";
                            break;
                        case (0x04):
                            errorDesc = "04 - Slave Device Failure";
                            break;
                        case (0x05):
                            errorDesc = "05 - Acknowledge";
                            break;
                        case (0x06):
                            errorDesc = "06 - Slave Device Busy";
                            break;
                        case (0x07):
                            errorDesc = "07 - Negative Acknowledge";
                            break;
                        case (0x08):
                            errorDesc = "08 - Memory Parity Error";
                            break;
                        case (0x0A):
                            errorDesc = "10 - Gateway Path Unavailable";
                            break;
                        case (0x0B):
                            errorDesc = "11 - Gateway Target Device Failed to Respond";
                            break;
                        default:
                            errorDesc = "Unknown error code";
                            break;
                    }
                    AddLog("Error code: " + errorDesc);
                    break;

            }
        }

        void AddLog(string message)
        {
            dateTime = DateTime.Now;
            TextBox_Log.Invoke((MethodInvoker)delegate { TextBox_Log.AppendText(Environment.NewLine + "[" + dateTime.Hour.ToString().PadLeft(2, '0') + ":" + dateTime.Minute.ToString().PadLeft(2, '0') + ":" + dateTime.Second.ToString().PadLeft(2, '0') + "] " + message); });
        }

        public static string ByteArrayToUnicode(byte[] input)
        {
            // stupid fucking WinForm textbox breaks from null symbols
            // stupid fucking Encoding class does byte-by-byte conversion
            List<char> result = new List<char>(input.Length / 2);
            byte[] flip = input;
            Array.Reverse(flip); // stupid fucking BitConverter is little-endian and spits out chinese nonsense otherwise
            for (int i = 0; i < flip.Length; i += 2)
            {
                result.Add(BitConverter.ToChar(flip, i));
            }
            result.Reverse();
            return new string(result.ToArray());
        }

        private void checkboxID_CheckedChanged(object sender, EventArgs e)
        {
            UpDown_ModbusID.Enabled = checkboxID.Checked ? true : false;
        }

        private void cBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cBoxDevice.SelectedItem)
            {
                case "Standard":
                    UpDown_ModbusID.Value = 30;
                    break;
                case "Premium Plus":
                    UpDown_ModbusID.Value = 30;
                    break;
                case "Inteli":
                    UpDown_ModbusID.Value = 26;
                    break;
                case "Premium":
                    UpDown_ModbusID.Value = 26;
                    break;
                default:
                    Console.WriteLine("Invalid change");
                    break;
            }
        }

        public Device GetDevice(DeviceType dt)
        {
            Device d = new Device();
            switch (dt)
            {
                case DeviceType.Standard:
                    d.modelName = "Standard";
                    d.name = "Gidrolock Standard Wi-Fi RS485";
                    d.id = 30;
                    d.modelName = "STW485";

                    d.valveStatus = new Entry(RegisterType.Coil, 1202);
                    d.alarmStatus = new Entry(RegisterType.Coil, 1201);

                    d.hasCleaningMode = true;
                    d.cleaningMode = new Entry(RegisterType.Coil, 3);

                    d.hasBattery = false;

                    d.wiredSensors = 2;
                    d.hasScenarioSensor = true;
                    d.sensorsAlarm = new Entry(RegisterType.Discrete, 1343, 24);

                    d.radioStatus = new Entry(RegisterType.Input, 1215, 21);

                    break;
                case DeviceType.Inteli:
                    d.modelName = "Inteli";
                    break;
                case DeviceType.PremiumPlus:
                    d.modelName = "Premium Plus";
                    break;
                case DeviceType.Premium:
                    d.modelName = "Premium";
                    break;
                default:
                    break;

            }
            return d;
        }
    }
}

public enum FunctionCode { ReadCoil = 1, ReadDiscrete = 2, ReadHolding = 3, ReadInput = 4, WriteCoil = 5, WriteRegister = 6, WriteMultCoils = 15, WriteMultRegisters = 16 };
//public enum SelectedPath { File, Folder };
public enum DeviceType { Standard, Inteli, PremiumPlus, Premium };
