namespace SmartHome;

public abstract class SmartDevice
{
    // 属性
    public string Name {get;set;}
    public string Room {get;set;} // 卧室,客厅 

    // 构造函数
    public SmartDevice(string name, string room)
    {
        Name = name;
        Room = room;
    }

    // 抽象方法：每个设备都必须能汇报自己的详细状态
    // 灯泡汇报亮度，空调汇报温度，报警器汇报正常
    public abstract void ShowStatus();
}