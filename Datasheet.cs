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

        SerialPort port = Modbus.port;
        bool isPolling = false;
        bool isAwaitingResponse = false;
        byte[] message = new byte[255];

        public Datasheet(byte modbusID, Device device)
        {
            nudModbusID.Minimum = 1;
            nudModbusID.Maximum = 246;
            this.modbusID = modbusID;
            this.device = device;
        }

        private async void buttonSetID_Click(object sender, EventArgs e)
        {
            Modbus.WriteSingleAsync(FunctionCode.WriteRegister, modbusID, 200, (byte)nudModbusID.Value);
            byte[] data = null;
            Modbus.ResponseReceived += (sndr, msg) =>
            {
                data = msg.Data;
            };
            await Task.Run(() =>
            {
                while (data is null) { continue; }
                if (message[1] > 0x10)  //exception code check
                    return;

            });
        }

        private void buttonValve_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonAlarm_Click(object sender, EventArgs e)
        {

        }
    }
}