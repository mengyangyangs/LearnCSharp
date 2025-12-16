// 烟雾报警器 不能继承接口类
namespace SmartHome;

public class SmokeSensor : SmartDevice
{
    // 特有属性
    // true 表示 安全
    public bool IsSafe {get; private set;}

    // [优化1] 构造函数简化
    // 默认买回来肯定是安全的
    public SmokeSensor(string name, string room) : base(name,room)
    {
        IsSafe = true;
    }

    // [优化2] 增加模拟触发方法
    // 供我们在 Program.cs 里测试用，模拟家里突然着火
    public void DetectSmoke()
    {
        IsSafe = false;
        Console.WriteLine($"🔥 [{Name}] 检测到浓烟！触发报警！");
    }

    // 复位方法（接触警报）
    public void Reset()
    {
        IsSafe = true;
        Console.WriteLine($"🛡️ [{Name}] 报警已解除，恢复正常监控!");
    }
    
    public override void ShowStatus()
    {
        if (IsSafe)
        {
            // 美化输出：把 True 变成中文
            Console.WriteLine($"[烟雾报警器] {Name} ({Room}) | 状态: 🟢 监测中 (安全)");
        }
        else
        {
            // 报警时醒目一点
            Console.WriteLine($"[烟雾报警器] {Name} ({Room}) | 状态: 🔴 警报中！！！");
            Console.WriteLine("🚨 哔哔哔！哔哔哔！🚨");
        }
    }
}