namespace MyFristApp;

// 手机不需要继承 Calculator类，它只需要签合同（ICalculator）就行
public class Phone:ICalculator
{
    public string Brand {get;set;}
    
    public Phone(string name)
    {
        Brand = name;
    }

    public void SayHello()
    {
        Console.WriteLine($"[Siri]:你好，我是{Brand}智能手机。");
    }

    // 必须履行合同：实现加减乘除
    public double Add(double a, double b){ return a + b;}
    public double Sub(double a, double b){ return a - b;}
    public double Mul(double a, double b){ return a * b;}
    public double Div(double a, double b){ if (b == 0) return 0; return a / b;}
    }