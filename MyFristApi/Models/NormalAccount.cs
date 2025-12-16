namespace MyFirstApi.Models; // 👈 命名空间

public class NormalAccount : IBankAccount
{
    // 👇👇👇 新增这一行：主键 ID 👇👇👇
    // EF Core 看到名字叫 "Id"，就会自动把它设为主键，并且自动增长 (1, 2, 3...)
    public int Id { get; set; }
    public string AccountName {get;set;}
    public double Balance {get; protected set;}

    // 👇👇👇 新增：一个人有一堆流水账单 👇👇👇
    // 初始化一下，防止空指针报错
    public List<TransactionRecord> Transactions {get;set;} = new List<TransactionRecord>();

    // 👇👇👇 新增这一行：无参构造函数（给 EF Core 用的） 👇👇👇
    public NormalAccount() { }

    public NormalAccount(string name,double initialMoney)
    {
        AccountName = name;
        Balance = initialMoney;
    }

    public virtual void Deposit(double amount)
    {
        if (amount <= 0)
            return;
        
        Balance += amount;

    }

    public virtual bool Withdraw(double amount)
    {
        if (amount <= 0 || Balance < amount) 
            return false;

        Balance -= amount;
        return true;
    }
    
}