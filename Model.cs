public class Device
{
    public string name;
    public byte id;
    public string modelName;
    public Entry firmware;
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

    public List<Entry> wspPlusMode; // Premium Plus only for now
    public List<Entry> wiredLineBreak;
    public Entry radioStatus;

    public List<ExclusiveEntry> exclusiveEntries = new();

    public static Device GetDevice(DeviceType dt)
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
                d.wiredLineBreak = new List<Entry>()
                    {
                        new Entry(RegisterType.Discrete, 1205),
                        new Entry(RegisterType.Discrete, 1207)
                    };
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
                d.wiredLineBreak = new List<Entry>()
                    {
                        new Entry(RegisterType.Discrete, 1205),
                        new Entry(RegisterType.Discrete, 1207)
                    };
                d.radioStatus = new Entry(RegisterType.Input, 1215, 21);

                break;
            case DeviceType.Inteli:
                d.modelName = "Inteli";
                d.id = 26;
                d.modelName = "INTELI";
                d.baudRate = new Entry(RegisterType.Holding, 129);
                break;
            case DeviceType.PremiumPlusWiFi:
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
                d.wspPlusMode = new List<Entry>()
                    {
                        new Entry(RegisterType.Coil, 1041),
                        new Entry(RegisterType.Coil, 1042),
                        new Entry(RegisterType.Coil, 1043),
                        new Entry(RegisterType.Coil, 1044),
                        new Entry(RegisterType.Coil, 1045),
                        new Entry(RegisterType.Coil, 1046),
                        new Entry(RegisterType.Coil, 1047),
                    };
                d.wiredLineBreak = new List<Entry>()
                    {
                        new Entry(RegisterType.Discrete, 1025),
                        new Entry(RegisterType.Discrete, 1026),
                        new Entry(RegisterType.Discrete, 1027),
                        new Entry(RegisterType.Discrete, 1028),
                        new Entry(RegisterType.Discrete, 1029),
                        new Entry(RegisterType.Discrete, 1030),
                        new Entry(RegisterType.Discrete, 1031),
                    };
                d.radioStatus = new Entry(RegisterType.Input, 1215, 21);
                break;
            case DeviceType.Premium:
                d.name = "Premium";
                d.id = 26;
                d.modelName = "BUP485";
                d.baudRate = new Entry(RegisterType.Holding, 0x06e);

                d.valveStatus = new Entry(RegisterType.Coil, 0x4B2);
                d.alarmStatus = new Entry(RegisterType.Coil, 0x4B1);

                d.hasCleaningMode = true;
                d.cleaningMode = new Entry(RegisterType.Coil, 0x003);

                d.hasBattery = true;
                d.batteryCharge = new Entry(RegisterType.Input, 0x064);

                d.wiredSensors = 7;
                d.sensorAlarm = new Entry(RegisterType.Input, 0x400, 7);
                break;
            case DeviceType.PremiumPlusLRS:
                d.name = "Premium Plus LRS";
                d.id = 0xf7;
                d.modelName = "GP+LRS";
                d.baudRate = new Entry(RegisterType.Input, 0x86);
                d.firmware = new Entry(RegisterType.Input, 0xfa, 6);

                d.valveStatus = new Entry(RegisterType.Coil, 0xa8);
                d.alarmStatus = new Entry(RegisterType.Coil, 0xa9);

                d.wiredSensors = 6;
                d.sensorAlarm = new Entry(RegisterType.Input, 0x8c, 6);
                d.wiredLineBreak = new List<Entry>()
                    {
                        new Entry(RegisterType.Input, 0x92),
                        new Entry(RegisterType.Input, 0x93),
                        new Entry(RegisterType.Input, 0x94),
                        new Entry(RegisterType.Input, 0x95),
                        new Entry(RegisterType.Input, 0x96),
                        new Entry(RegisterType.Input, 0x97),
                    };
                d.exclusiveEntries =
                    [
                    new(RegisterType.Input, 0x8b, DataFormat.UInt16, "Ток в цепи приводов"),
                    new(RegisterType.Input, 0x98, DataFormat.UInt16, "Статус крана"),
                    new(RegisterType.Input, 0x9a, DataFormat.UInt16, "Протечка в зонах"),
                    new(RegisterType.Input, 0x9c, DataFormat.UInt16, "Ошибка крана"),
                    new(RegisterType.Input, 0x9d, DataFormat.UInt16, "Дней после проворота"),
                    new(RegisterType.Input, 0x9e, DataFormat.UInt16, "Часов до проворота"),
                    new(RegisterType.Input, 0x9f, DataFormat.UInt16, "Минут до проворота"),
                    new(RegisterType.Coil, 0xaa, DataFormat.Bool, "Проворот")
                    ];

                break;
            default:
                break;

        }
        return d;
    }
}

public class Entry
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
public class ExclusiveEntry : Entry
{
    public DataFormat dataFormat;
    public string name;
    public ExclusiveEntry(RegisterType registerType, ushort address, DataFormat dataFormat, string name, ushort length = 1) : base(registerType, address, length)
    {
        this.registerType = registerType;
        this.address = address;
        this.length = length;
        this.dataFormat = dataFormat;
        this.name = name;
    }
}
public enum DataFormat { Bool, UInt16, UInt32, String };
public enum RegisterType : byte { Coil = 1, Discrete = 2, Holding = 3, Input = 4 }
public enum DeviceType : byte { StandardWifi, StandardRadio, PremiumPlusWiFi, Inteli, Premium, PremiumPlusLRS };