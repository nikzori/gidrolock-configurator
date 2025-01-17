using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gidrolock_Modbus_Scanner
{
    public class Device : 
    {
        public string name;
        public byte id;
        public string modelName;
        public List<Entry> entries;

        public Device(string name, string modelName)
        {
            this.name = name;  
        }

    }

    public interface IHasWiredSensors
    {
        Entry GetAlarmStatuses();
        Entry GetSignalLoss();
    }
    public interface IHasWirelessSensors
    {
        Entry GetWirelessSensors();
        Entry GetSensors();
    }
    public interface IHasEmergencyOpen
    {
        void EmergencyOpen();
    }

    public struct Entry
    {
        public string name;
        public RegisterType registerType;
        public ushort address;
        public ushort length;
        public string dataType;
        public List<string> labels;
        public Dictionary<string, string> valueParse;
        public bool readOnce;
        public bool isModbusID;

        public Entry(string name, RegisterType registerType, ushort address, ushort length = 1, string dataType = "uint16", List<string> labels = null, Dictionary<string, string> valueParse = null, bool readOnce = false, bool isModbusID = false)
        {
            this.name = name;
            this.registerType = registerType;   
            this.address = address;
            this.length = length;
            this.dataType = dataType;
            this.labels = labels;
            this.valueParse = valueParse;

            this.readOnce = readOnce;
            this.isModbusID = isModbusID;
        }
    }
    public struct CheckEntry
    {
        public RegisterType registerType;
        public ushort address;
        public ushort length;
        public string dataType;
        public string expectedValue;
        public CheckEntry(RegisterType registerType, ushort address, ushort length, string dataType, string expectedValue)
        {
            this.registerType = registerType;
            this.address = address;
            this.length = length;
            this.dataType = dataType;
            this.expectedValue = expectedValue;
        }
    }
    public enum RegisterType { Coil = 1, Discrete = 2, Holding = 3, Input = 4}
}
