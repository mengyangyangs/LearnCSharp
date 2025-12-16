// 智能灯
namespace SmartHome;

public class SmartLight : SmartDevice , ISwitchable
{
    // 特有属性
    // [修改点1] 建议加上 private set，防止外部随意修改亮度，只能通过开关调节
    public int Brightness {get; private set;} 

    // 接口规定属性，必须显式声明出来
    // 默认打开
    public bool IsOn {get; private set;}

    // [修改点2]构造函数简化
    // 我们不需要让用户传亮度及开关状态，默认买回来都是关着且亮度为0的
    public SmartLight(string name, string room) : base(name, room)
    {
        Brightness = 0;
        IsOn = false;
    }

    public override void ShowStatus()
    {
        // 这里用了一个小技巧：三元运算符（IsOn ? "开" : "关")
        Console.WriteLine($"[家居]:{Name},在{Room}中的状态 | {(IsOn ? "开":"关")} | 亮度是:{Brightness}%");
    }

    // 开
    public void TurnOn()
    {
        // [修改点3]卫语句（Guard Clause）
        // 先判断如果不满足条件直接return，这样下面就不需要写else了
        if (IsOn)
        {
            Console.WriteLine("开关已经打开...");
            return;
        }

        // [修改点4]
        // 开灯不仅要改状态，还要恢复默认亮度
        IsOn = true;
        Brightness = 50;
        Console.WriteLine($"💡 {Name}已开启！亮度自动设置为:{Brightness}%");
    }

    // 关
    public void TurnOff()
    {
        if (!IsOn)
        {
            Console.WriteLine("开关已经关闭...");
            return;
        }
        
        // [修改点4]补全业务逻辑
        // 关灯后，亮度应该归零
        IsOn = false;
        Brightness = 0;
        Console.WriteLine($"💡 {Name}已关闭...");
    }

}