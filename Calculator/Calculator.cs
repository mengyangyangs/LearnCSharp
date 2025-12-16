namespace MyFristApp;

public class Calculator : ICalculator
{
    // 属性的语法: public 类型 名字 {get;set;}
    // {get;set;}代表这是一个标准的属性
    public string Brand {get;set;}

    // 构造函数:必须和类名完全一样
    // 只要 new 类名()，这个方法就会跑
    public Calculator(string name)
    {
        // 把传入的名字，贴到自己Brand属性上
        Brand = name;
    }

    // 新增方法：自我介绍
    // 关键点：virtual 关键字，意思是我允许子类修改这个方法
    public virtual void SayHello()
    {
        Console.WriteLine($"大家好，我是{Brand},我会做基础运算!");
    }
    public double Add(double a, double b)
    {
        return a + b;
    }
    public double Sub(double a, double b)
    {
        return a - b;
    }
    public double Mul(double a, double b)
    {
        return a * b;
    }
    public double Div(double a, double b)
    {
        if (b == 0)
        {
            return 0; // 防止除数为零
        }
        return a / b;
    }
}