using System;
using System.Collections.Generic;

namespace Gidrolock_Modbus_Scanner
{
    public class Device
    {
        public string name;
        public byte id;
        public string modelName;
        public Entry baudRate;

        public Entry valveStatus;
        public Entry alarmStatus;

        public bool hasCleaningMode;
        public Entry cleaningMode;

        public bool hasBattery;
        public Entry batteryCharge;
        public Entry batteryUsed; // питание от баттареи/электросети для устройств с баттареями

        public int wiredSensors;
        public bool hasScenarioSensor;
        public Entry sensorAlarm;

        public List<Entry> wiredLineBreak;

        public Entry radioStatus;
    }

    public struct Entry
    {
        public RegisterType registerType;
        public ushort address;
        public ushort length;

        public Entry(RegisterType registerType, ushort address, ushort length = 1)
        {
            this.registerType = registerType;   
            this.address = address;
            this.length = length;
        }
    }
    public enum RegisterType { Coil = 1, Discrete = 2, Holding = 3, Input = 4}
}
