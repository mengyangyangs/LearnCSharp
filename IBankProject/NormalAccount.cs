namespace MyFristApp;
// 【知识点复习】：类实现接口 (Class : Interface)
// NormalAccount 遵守 IBankAccount 的合同

public class NormalAccount : IBankAccount
{
    // 1.----属性----
    // {get;set;} 表示既能读也能写
    public string Accountname {get;set;}

    // {get; protected set;}
    // public get : 外面的人可以看余额
    // protected set : 只有自己（和我的子类）能改余额，Program.cs 这种外人不能直接修改
    public double Balance { get; protected set;}

    // 2.----构造函数----
    //【知识点复习】：构造函数
    // 1. 名字必须和类名一样。
    // 2. 在 new 的时候自动执行，用于初始化（出厂设置）。
    public NormalAccount(string name,double initialMoney)
    {
        Accountname = name;
        Balance = initialMoney;
    }

    // 3.----方法----
    // 【知识点复习】：Virtual (虚方法)
    // 加上 virtual，表示“我允许子类修改这个方法的逻辑”。
    // 如果不加 virtual，子类就只能照搬，不能修改。
    public virtual void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("存款金额必须大于0！");
            return;
        }

        // 余额增加
        Balance += amount;
        Console.WriteLine($"{Accountname}存入了{amount}元，当前余额为{Balance}元。");
    }

    // 取款逻辑
    // 返回bool(真/假)来告诉调用者，这次取款是否成功
    public virtual bool Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("取款金额必须大于0！");
            return false; // 失败
        }

        if (Balance >= amount) // 钱够不够
        {
            Balance -= amount;
            Console.WriteLine($"{Accountname}取出了{amount}元。当前余额为{Balance}元。");
            return true; // 成功
        }
        else
        { 
            Console.WriteLine("余额不足，取款失败!"); return false; // 失败
        }
    }
}