namespace MyFristApp;

// 继承Calculator类
public class ScientificCalculator : Calculator
{
    // 构造函数
    // 必须接住name，然后用 : base(name) 把它传给父亲
    public ScientificCalculator(string name) : base(name)
    {
    }

    // 重写方法：自我介绍
    // 关键点：override 关键字。意思是"我要覆盖父类的SayHello()方法“
    public override void SayHello()
    {
        base.SayHello(); // 先调用父类的方法
        
        Console.WriteLine("=================");
        Console.WriteLine($"警告⚠️:{Brand}已启动！");
        Console.WriteLine("搭载科学计算核心，已支持幂运算(Power)功能!");
        Console.WriteLine("=================");
    }
    public double Power(double a, double b)
    {
        // Math.Pow 是 C# 自带的数学工具
        return Math.Pow(a,b);
    }
}