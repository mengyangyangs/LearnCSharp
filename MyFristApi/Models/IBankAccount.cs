namespace MyFirstApi.Models; // 👈 命名空间

public interface IBankAccount
{
    string AccountName { get; set; }
    double Balance { get; }
    void Deposit(double amount);
    bool Withdraw(double amount);
}