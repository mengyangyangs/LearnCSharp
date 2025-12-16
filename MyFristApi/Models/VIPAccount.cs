namespace MyFirstApi.Models;

public class VIPAccount : NormalAccount
{
    // 👇👇👇 新增这一行：无参构造函数 👇👇👇
    public VIPAccount() { }
    public VIPAccount(string name,double initialMoney) : base(name,initialMoney)
    {
    }

    public override void Deposit(double amount)
    {
        if (amount <= 0) return;
        // VIP特权:送10%
        double interest = amount * 0.1;
        Balance += (interest + amount);

    }

}