using Gidrolock_Modbus_Scanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gidrolock_Modbus_Configurator
{
    public class Update
    {
        bool isAwaitingResponse = false;
        ModbusResponseEventArgs latestMessage;
        SerialPort port = Modbus.port;

        public void UpdateFirmware(Stream fileStream)
        {
            Modbus.ResponseReceived += (sndr, msg) => { isAwaitingResponse = false; latestMessage = msg; };
            int offset = 0;
            byte[] buffer;
            byte[] preCRC;
            byte[] message;
            byte[] CRC = new byte[2];

            long bytesLeft = fileStream.Length;
            
            while (bytesLeft > 0)
            {
                int count = bytesLeft > 64 ? 64 : (int)bytesLeft;
                buffer = new byte[count];

                fileStream.Read(buffer, offset, count);

                preCRC = new byte[7 + buffer.Length];

                preCRC[0] = 0x1E; // Modbus ID
                preCRC[1] = 0x10; // Function code
                preCRC[2] = 0xFF; // Address 01
                preCRC[3] = 0xFF; // Address 02
                preCRC[4] = 0x00; // Cnt01
                preCRC[5] = 0x21; // Cnt02
                preCRC[6] = 0x42; // byte count

                for (int i = 0; i < count; i++)
                {
                    preCRC[i+7] = buffer[i];
                }
                Modbus.GetCRC(preCRC, ref CRC);
                message = new byte[preCRC.Length + 2];

                for (int i = 0; i < preCRC.Length; i++)
                {
                    message[i] = preCRC[i];
                }
                message[message.Length - 2] = CRC[0];
                message[message.Length - 1] = CRC[1];

                isAwaitingResponse = true;
                port.Write(message, 0, message.Length);

                var delay = Task.Delay(2000).ContinueWith(_ =>
                {
                    if (isAwaitingResponse)
                    {
                        MessageBox.Show("Превышено время ожидания ответа от устройства.");
                        isAwaitingResponse = false;
                    }
                });
                while (isAwaitingResponse) { continue; }

                if (latestMessage != null && latestMessage.Status != ModbusStatus.Error)
                {
                    bytesLeft -= count;
                    offset += count;
                }                
            }
        }
    }
}
