// 空调
namespace SmartHome;

public class AirConditioner : SmartDevice,ISwitchable
{
    // [修改点1]特有属性：温度
    // 建议 private set，防止外部直接修改
    public int Temperature {get; private set;}

    // 接口定义属性
    public bool IsOn {get; private set;}

    // [修改点2]简化构造函数
    // 默认关机，默认温度26
    public AirConditioner(string name, string room) : base(name, room)
    {
        Temperature = 26;
        IsOn = false;
    }

    // 特有方法
    // [修改点3] 逻辑：你给我一个目标温度，我先检查一下是否开机，然后再设置
    public void SetTemperature(int targeTemp)
    {
        if (!IsOn)
        {
            Console.WriteLine("🥶 空调未开启，无法调节温度...");
            return;
        }
        // 简单的逻辑：直接设置
        // 实际开发中可能还会检查温度范围(例如16～30)
        if (targeTemp < 16 || targeTemp > 30)
        {
            Console.WriteLine("❌ 温度设置无效！只能在16-30度之间...");
            return;
        }

        Temperature = targeTemp;
        Console.WriteLine($"🥶 {Name}温度已调节为:{Temperature}");
            
    }

    // [修改点4] 使用三元运算符
    public override void ShowStatus()
    {
        Console.WriteLine($"[智能空调] {Name} ({Room}) | 状态: {(IsOn ? "开" : "关")} | 温度: {Temperature}°C");
    }

    // 开
    // [修改点5] 卫语句优化 TurnOn
    public void TurnOn()
    {
        if (IsOn)
        {
            Console.WriteLine("开关已经成功打开！");
            return;
        }
        IsOn = true;
        // 开机时，保持上次的温度或者重置为26度均可
        Console.WriteLine($"🥶 {Name}已开启！当前温度:{Temperature}°C");
    }

    // 关
    // [修改点5] 卫语句优化 TurnOff
    public void TurnOff()
    {
        if (!IsOn)
        {
            Console.WriteLine($"🌑 {Name} 已经是关着的了...");
            return;
        }
        
        IsOn = false;
        Console.WriteLine($"🌑 {Name}已关闭...");
    }

}