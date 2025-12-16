namespace MyFristApp;
// 【知识点复习】：继承 (Inheritance)
// VIPAccount 是 NormalAccount 的“儿子”，自动拥有了名字、余额、取款功能。
public class VIPAccount : NormalAccount
{
    // ---构造函数---
    // [知识点复习]：base
    // 子类出生时，必须先把参数传给父类的构造函数，让父类先完成初始化
    public VIPAccount(string name, double initialMoney) : base(name,initialMoney)
    {
        // 可以在这里写VIP特有的初始化代码，如果没别的空着也行
    
    }
    // ----方法重写----
    // 【知识点复习】：Override (重写)
    // 对应父类的 virtual 方法。我们要推翻父类的“普通存钱”逻辑，换成“VIP存钱”逻辑。
    public override void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("存款金额必须大于0！");
            return;
        }
        // VIP特权：额外送10%
        double interest = amount * 0.1;
        double totalDeposit = amount + interest;

        // 这里我们直接修改父类的Balance属性（因为它设置了protected set，所以子类能直接修改
        Balance = Balance + totalDeposit;

        Console.WriteLine($"[VIP特权]尊贵的{Accountname},您存入了{amount}元，银行赠送{interest}元");
        Console.WriteLine($"实际入帐{totalDeposit}元。当前余额：{Balance}元");
        
    }

    // 注意：我们要不要重写Withdraw（取款）？
    // 如果VIP取款规则和普通人一样，就不需要写override，直接用父类的就好
    // 这就是继承的好处：复用代码
}