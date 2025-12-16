using Microsoft.EntityFrameworkCore; // 引入 EF Core 工具包
using MyFirstApi.Models; // 引入你的模型（比如 NormalAccount）

namespace MyFirstApi.Data;

// 继承 DbContext，说明这个类是“数据库管理员”
public class DataContext : DbContext
{
    // 1. 构造函数：固定写法
    // 意思：接收外部的配置（比如告诉它数据库文件在哪里），然后传给父类
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    // 2. 定义“表”
    // DbSet<T> 就相当于数据库里的一张表
    // 以前你在 Controller 里写 List<NormalAccount>
    // 现在你在 Admin 里写 DbSet<NormalAccount>
    // 数据库会自动创建一张叫 "Accounts" 的表，里面存 NormalAccount
    public DbSet<NormalAccount> Accounts { get; set; }

    // 👇👇👇 新增：交易记录表 👇👇👇
    public DbSet<TransactionRecord> Transactions {get;set;}

    // 👇👇👇 新增：信用卡表 👇👇👇
    // 这一行代码，决定了数据库里会多出一张叫“CreditCards“的表
    public DbSet<CreditCard> CreditCards {get;set;}
}