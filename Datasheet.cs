using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Gidrolock_Modbus_Scanner
{
    /*
     * This is more of a View than a View-Controller
     * Relegate everything to interface functions for models
     */
    public partial class Datasheet : Form
    {
        byte modbusID;
        Device device;

        byte[] message;
        byte[] data;
        EventHandler<ModbusResponseEventArgs> handler;

        SerialPort port = Modbus.port;
        bool isPolling = false;
        bool isAwaitingResponse = false;


        bool isValveClosed = false;
        bool alarmStatus = false;
        bool cleaningStatus = false;

        public Datasheet(byte modbusID, Device device)
        {
            nudModbusID.Minimum = 1;
            nudModbusID.Maximum = 246;
            this.modbusID = modbusID;
            this.device = device;

            if (!device.hasCleaningMode)
            {
                labelCleaning.Text = "Недоступна.";
                buttonCleaning.Enabled = false;
            }

            handler = (sndr, msg) =>
            {
                message = msg.Message;
                data = msg.Data;
            };

        }

        private async void buttonPoll_Click(object sender, EventArgs e)
        {
            // hardcoded for now, probably easier to keep it like this in the future
            if (await PollEntry(device.valveStatus))
            {
                if (data.Last() > 0)
                    labelValve.Text = "Закрыт";
                else labelValve.Text = "Открыт";
            }
        }

        async Task<bool> PollEntry(Entry entry)
        {
            var send = Modbus.ReadRegAsync(modbusID, (FunctionCode)entry.registerType, entry.address, entry.length);
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
        private async void buttonSetID_Click(object sender, EventArgs e)
        {
            Modbus.WriteSingleAsync(FunctionCode.WriteRegister, modbusID, 200, (byte)nudModbusID.Value);

            await Task.Run(() =>
            {
                while (data is null) { continue; }
                if (message[1] > 0x10)  //exception code check
                    return;
                this.modbusID = data[1]; //no exception, can just take the byte
                message = null;
                data = null;
            });
            
        }

        private void buttonValve_Click(object sender, EventArgs e)
        {
            ushort value = isValveClosed ? (ushort)0 : (ushort)1;
            Modbus.WriteSingleAsync((FunctionCode)device.valveStatus.registerType, modbusID, device.valveStatus.address, value);
        }

        private void buttonAlarm_Click(object sender, EventArgs e)
        {
            ushort value = alarmStatus ? (ushort)0 : (ushort)1;
            Modbus.WriteSingleAsync((FunctionCode)device.valveStatus.registerType, modbusID, device.alarmStatus.address, value);
        }

        private void buttonCleaning_Click(object sender, EventArgs e)
        {

        }       
    }
}