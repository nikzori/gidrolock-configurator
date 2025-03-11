using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Reflection;
using System.Diagnostics;

namespace Gidrolock_Modbus_Scanner
{
    public partial class App : Form
    {

        public bool isAwaitingResponse = false;
        public short[] res = new short[12];
        SerialPort port = Modbus.port;
        public int expectedLength = 0;
        ModbusResponseEventArgs latestMessage;

        public Dictionary<string, string> models = new Dictionary<string, string>();

        DateTime dateTime;

        Datasheet datasheet;
        Stopwatch stopwatch = new Stopwatch();
        #region Initialization
        public App()
        {
            InitializeComponent();
            Modbus.Init();
            Modbus.ResponseReceived += OnResponseReceived;

            this.upDownModbusID.Value = 0;
            TextBox_Log.Text = "Приложение готово к работе.";

            cBoxSpeed.Items.Add("1200");
            cBoxSpeed.Items.Add("2400");
            cBoxSpeed.Items.Add("4800");
            cBoxSpeed.Items.Add("9600");
            cBoxSpeed.Items.Add("14400");
            cBoxSpeed.Items.Add("19200");
            cBoxSpeed.Items.Add("38400");
            cBoxSpeed.Items.Add("57600");
            cBoxSpeed.Items.Add("115200");
            cBoxSpeed.SelectedIndex = 3;

            upDownModbusID.Value = 30;
            upDownModbusID.Minimum = 1;
            upDownModbusID.Maximum = 247;

            models.Add("Standard Wi-Fi", "STW485");
            models.Add("Standard Radio", "STR485");
            models.Add("Premium Plus", "PRPLS1");
            models.Add("Inteli", "INTELI");
            models.Add("Premium", "BUP485");

            /* - Version Check - */
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Console.WriteLine("Version: " + version);
        }

        void App_FormClosed(object sender, FormClosedEventArgs e)
        {
            port.Close();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            cBoxPorts.Items.AddRange(SerialPort.GetPortNames());
            if (cBoxPorts.Items.Count > 0)
                cBoxPorts.SelectedIndex = 0;
        }
        #endregion


        private async void ButtonConnect_Click(object sender, EventArgs e)
        {

            if (cBoxPorts.SelectedItem.ToString() == "COM1")
            {
                DialogResult res = MessageBox.Show("Выбран серийный порт COM1, который обычно является портом PS/2 или RS-232, не подключенным к Modbus устройству. Продолжить?", "Внимание", MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                    return;
            }
            if (upDownModbusID.Value == 0)
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
                port.PortName = cBoxPorts.Text;
                port.BaudRate = Int32.Parse(cBoxSpeed.Items[cBoxSpeed.SelectedIndex].ToString());
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


                await Task.Run(async () =>
                {
                    // send message
                    latestMessage = null;
                    isAwaitingResponse = true;
                    var send = Modbus.ReadRegAsync((byte)upDownModbusID.Value, FunctionCode.ReadInput, 200, 6);
                    stopwatch.Restart();
                    int counter = 0;
                    while (isAwaitingResponse)
                    {
                        if (stopwatch.ElapsedMilliseconds > 1000)
                        {
                            AddLog("Истекло время ожидания ответа от устройства. Повторный запрос...");
                            isAwaitingResponse = false;
                            counter++;
                            if (counter > 3)
                            {
                                AddLog("Устройство не отвечает. Проверьте соединение с устройством.");
                                return;
                            }
                        }
                    }

                    if (latestMessage is null)
                    {
                        Console.WriteLine("Latest message is null;");
                        return;
                    }
                    if (latestMessage.Status == ModbusStatus.Error)
                        return;


                    // confirm the model
                    string response = ByteArrayToUnicode(latestMessage.Data);
                    int index = 0;
                    string match = "";
                    string model = "";
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

                    else // match found
                    {
                            // instantiate panel
                            AddLog("Открываю панель конфигурации.");

                            Device device = GetDevice((DeviceType)index);
                            StartDatasheet(device);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void StartDatasheet(Device device)
        {
            datasheet = new Datasheet((byte)upDownModbusID.Value, device);
            Application.Run(datasheet);
        }


        void CBox_Ports_Click(object sender, EventArgs e)
        {
            cBoxPorts.Items.Clear();
            cBoxPorts.Items.AddRange(SerialPort.GetPortNames());
        }

        void OnResponseReceived(object sender, ModbusResponseEventArgs e)
        {
            latestMessage = e;
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
            //Array.Reverse(flip); // stupid fucking BitConverter is little-endian and spits out chinese nonsense otherwise
            for (int i = 0; i < flip.Length; i++)
            {
                if (flip[i] == 0x00)
                    continue;
                else result.Add(Convert.ToChar(flip[i]));
            }
            //result.Reverse();
            return new string(result.ToArray());
        }

        public Device GetDevice(DeviceType dt)
        {
            Device d = new Device();
            switch (dt)
            {
                case DeviceType.StandardWifi:
                    d.name = "Standard Wi-Fi RS485";
                    d.id = 30;
                    d.modelName = "STW485";
                    d.firmware = new Entry(RegisterType.Input, 250, 6);
                    d.baudRate = new Entry(RegisterType.Holding, 110);

                    d.valveStatus = new Entry(RegisterType.Coil, 1202);
                    d.alarmStatus = new Entry(RegisterType.Coil, 1201);

                    d.hasCleaningMode = true;
                    d.cleaningMode = new Entry(RegisterType.Coil, 3);

                    d.hasBattery = false;

                    d.wiredSensors = 2;
                    d.hasScenarioSensor = true;
                    d.sensorAlarm = new Entry(RegisterType.Discrete, 1343, 24);

                    d.radioStatus = new Entry(RegisterType.Input, 1215, 21);

                    break;
                case DeviceType.StandardRadio:
                    d.name = "Standard Radio RS485";
                    d.id = 30;
                    d.modelName = "STR485";
                    d.firmware = new Entry(RegisterType.Input, 250, 6);
                    d.baudRate = new Entry(RegisterType.Holding, 110);

                    d.valveStatus = new Entry(RegisterType.Coil, 1202);
                    d.alarmStatus = new Entry(RegisterType.Coil, 1201);

                    d.hasCleaningMode = true;
                    d.cleaningMode = new Entry(RegisterType.Coil, 3);

                    d.hasBattery = false;

                    d.wiredSensors = 2;
                    d.hasScenarioSensor = false;
                    d.sensorAlarm = new Entry(RegisterType.Discrete, 1343, 23);

                    d.radioStatus = new Entry(RegisterType.Input, 1215, 21);

                    break;
                case DeviceType.Inteli:
                    d.modelName = "Inteli";
                    d.id = 26;
                    d.modelName = "INTELI";
                    d.baudRate = new Entry(RegisterType.Holding, 129);
                    break;
                case DeviceType.PremiumPlus:
                    d.name = "Premium Plus Wi-Fi";
                    d.id = 30;
                    d.modelName = "PRPLS1";
                    d.firmware = new Entry(RegisterType.Input, 250, 6);
                    d.baudRate = new Entry(RegisterType.Holding, 110);

                    d.valveStatus = new Entry(RegisterType.Coil, 1202);
                    d.alarmStatus = new Entry(RegisterType.Coil, 1201);

                    d.hasCleaningMode = true;
                    d.cleaningMode = new Entry(RegisterType.Coil, 3);

                    d.hasBattery = true;
                    d.batteryCharge = new Entry(RegisterType.Input, 100);

                    d.wiredSensors = 7;
                    d.hasScenarioSensor = true;
                    d.sensorAlarm = new Entry(RegisterType.Discrete, 1343, 29);
                    d.radioStatus = new Entry(RegisterType.Input, 1215, 21);
                    break;
                case DeviceType.Premium:
                    d.modelName = "Premium";
                    d.id = 26;
                    d.modelName = "BUP485";
                    d.baudRate = new Entry(RegisterType.Holding, 110);
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


public enum DeviceType { StandardWifi, StandardRadio, PremiumPlus, Inteli, Premium };
