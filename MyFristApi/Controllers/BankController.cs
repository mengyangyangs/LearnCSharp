// using Microsoft.AspNetCore.Mvc; // 必须引入这个，才能当Controller
// using MyFirstApi.Models; // 引入刚才的类

// namespace MyFirstApi.Controllers;

// [ApiController] // 1.给这个类贴上标签：你是处理APi的
// [Route("[controller]")] // 2.规定网址：网址就是类名去掉Controller(即 /Bank)
// public class BankController : ControllerBase
// {
//     // 旧方法（1.0）：为了方便演示，我们在内存里存一个[静态]的VIP帐户
//     // static意味着：不管你刷新多少次网页，只要服务器不重启，它就是同一个帐户，钱会累加！
//     // private static IBankAccount _myAccount = new VIPAccount("马帅",100);

//     // 旧方法（2.0）：使用列表List代替原来的单个对象
//     // static 保证所有请求共用这一份名单
//     // 至于类型为什么是IBankAccount类，原因是如果单写VIPAccount或者NormalAccount的话，那么其中一个就用不上了
//     private static List<IBankAccount> _accounts = new List<IBankAccount>();

//     // ---新业务1:开户(OpenAccount)
//     // POST/Bank/OpenAccount
//     // 参数：name（名字），type（1:普通，2:VIP），money（初始金额）
//     [HttpPost("OpenAccount")]
//     public string OpenAccount(string name,string type,double money)
//     {
//         // 先检查是否同名的人(防止重名)
//         // Find:在列表里找。x=>x.AccountNane == name 是查找条件
//         var existing = _accounts.Find( x => x.AccountName == name);
//         if (existing != null)
//         {
//             return $"开户失败：用户{name}已经存在！";
//         }

//         // 创建新用户
//         IBankAccount newAcc; 
//         if (type == "2")
//         {
//             newAcc = new VIPAccount(name,money);
//         }
//         else
//         {
//             newAcc = new NormalAccount(name,money);
//         }

//         // 加入列表
//         _accounts.Add(newAcc);
//         return $"开户成功！欢迎{name}加入本行。当前余额为:{money}";
//     }

//     // 新业务2：查询余额
//     // GET/Bank/Balance?name=小马
//     // 我们需要知道查谁的余额
//     [HttpGet("Balance")]
//     public string GetBalance(string name)
//     {
//         // 1.先去列表里把这个人找出来
//         var acc = _accounts.Find( x => x.AccountName == name);

//         // 2.如果没找到
//         if (acc == null) return "查无此人，请先开户！";

//         // 3.找到了，返回他的余额
//         return $"尊贵的{acc.AccountName},您的余额为:{acc.Balance}";
//     }

//     // ---业务3：存款
//     // POST/Bank/Deposti?name=小马&amount=100
//     [HttpPost("Deposit")]
//     public string Deposit(string name,double amount)
//     {
//         var acc = _accounts.Find( x => x.AccountName == name);
//         if (acc == null) return "查无此人";

//         // 调用存款方法（多态会自动生效）
//         acc.Deposit(amount);
//         return $"存款成功！{name}最新的余额为:{acc.Balance}";
//     }

//     // ---新业务4：取款
//     // POST/Bank/Withdraw?name=小马&amount=50
//     [HttpPost("Withdraw")]
//     public string Withdraw(string name,double amount)
//     {   
//         var acc = _accounts.Find( x => x.AccountName == name);
//         if (acc == null) return "查无此人";

//         bool success = acc.Withdraw(amount);
//         if (success)
//         {
//             return $"取款成功！{name}最新的余额:{acc.Balance}";
//         }
//         else
//         {
//             return "取款失败，余额不足.";
//         }
//     }

//     // ---新业务5：看看银行到底有多少人
//     // GET /Bank/All
//     [HttpGet("All")]
//     public List<string> GetAllUsers()
//     {
//         // 这一句有点高级：把所有帐户的名字“投影”出来变成一个新列表
//         // Select 是C# LINQ的神技
//         return _accounts.Select(x => $"{x.AccountName} (余额:{x.Balance})").ToList();
//     }
// }

// 连接数据库的写法
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Data; // 👈 1.引入数据库管理员的命名空间
using System.Linq;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.Query; // 引入查询工具
using Microsoft.EntityFrameworkCore;

namespace MyFirstApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BankController : ControllerBase
{
    // ❌ 以前:自己维护一个静态列表
    // private static List<IBankAccount> _accounts = new List<IBankAccount>();

    // ✅ 现在:声明一个数据库管理员
    private readonly DataContext _context;

    // 2.构造函数注入(Dependency Injection)
    // 意思:当有人找 BankController 办事时，系统自动派一个管理员(_context)给他用
    public BankController(DataContext context)
    {
        _context = context;
    }

    // --- 业务1:开户 ---
    [HttpPost("OpenAccount")]
    public string OpenAccount(string name,string type,double money)
    {
        // 1.去数据库里查
        // _context.Accounts 就是数据库里的那张表
        // FirstOrDefault 意思是：找第一个符合条件的，找不到就返回 null
        var existing = _context.Accounts.FirstOrDefault(x => x.AccountName == name);

        if (existing != null)
        {
            return $"开户失败:用户{name}已经存在";
        }

        // 2.创建对象
        NormalAccount newAcc;
        if (type == "2")
        {
            newAcc = new VIPAccount(name,money);
        }
        else
        {
            newAcc = new NormalAccount(name,money);
        }

        // 👇👇👇 新增：创建第一笔流水 👇👇👇
        TransactionRecord firstLog = new TransactionRecord
        {
            Amount = money,
            NewBalance = money,
            TransactionDate = DateTime.Now,
            Account = newAcc // 直接把对象挂上去，EF Core 会自动处理外键
        };

        // 这里有个神奇的地方：
        // 我们只需要把newAcc加进去，因为 firstLog 挂在newAcc身上
        // 或者是把 firstLog 加进去
        // 最稳妥的方法是：
        // 3.存入数据库(关键步骤！)
        _context.Accounts.Add(newAcc); // 先把人的申请单填好
        _context.Transactions.Add(firstLog); // 加流水
        _context.SaveChanges(); // 盖章生效！这一步才真正会写入硬盘

        return $"开户成功！欢迎 {name}。ID: {newAcc.Id} (由数据库自动生成)";
    }

    // ---业务2:查询余额---
    [HttpGet("Balance")]
    public object GetBalance(string name)
    {
        Console.WriteLine($"侦探报告：正在查找的名字是 [{name}]");

        // 👇👇👇 关键：Include(x => x.Transactions) 👇👇👇
        // 意思：查这个人的时候，顺便把它的 Transactions 列表也抓取出来
        // 去数据库找人
        var acc = _context.Accounts
                .Include(x => x.Transactions) 
                // 👇 修改点：加上 Trim() 去掉空格
                .FirstOrDefault(x => x.AccountName.Trim() == name.Trim());

        if (acc == null)
        {
            return "查无此人!";
        }
        // 旧写法：（只返回一句话）
        // return $"尊贵的{ acc.AccountName }，您的余额为:{acc.Balance}";
        
        // 新写法：返回一个数据包（匿名对象）
        // Web API 会自动变成好看的Json格式
        return new
        {
            Message = $"尊贵的 {acc.AccountName}，您的余额为: {acc.Balance}", // 保留你的问候语
            History = acc.Transactions.Select(t => new { 
            Time = t.TransactionDate,
            Type = t.Amount > 0 ? "存款" : "取款",
            Amount = t.Amount,
            BalanceAfter = t.NewBalance
        })
        };
    }
    
    // ---业务3:存款---
    [HttpPost("Deposit")]
    public string Deposit(string name,double amount)
    {
        var acc = _context.Accounts.FirstOrDefault(x => x.AccountName == name);
        if (acc == null) return "查无此人!";

        // 修改内存里的数据
        acc.Deposit(amount);

        // 👇👇👇 新增：记流水 👇👇👇
        var log = new TransactionRecord
        {
            Amount = amount,
            NewBalance = acc.Balance,
            TransactionDate = DateTime.Now,
            AccountId = acc.Id // 也可以直接填Id
        };
        _context.Transactions.Add(log);
        // 关键：告诉数据库数据变了，请保存
        _context.SaveChanges();
        
        return $"存款成功！当前余额:{acc.Balance}";
    }

    // ---业务4:取款---
    [HttpPost("Withdraw")]
    public string Withdraw(string name,double amount)
    {
        var acc = _context.Accounts.FirstOrDefault(x => x.AccountName == name);
        if (acc == null) return "查无此人！";

        bool success = acc.Withdraw(amount);
        if (success)
        {
            // 取款成功，记得保存到数据库
            _context.SaveChanges();
            return $"取款成功！当前余额:{acc.Balance}";
        }
        else
        {
            return "取款失败，余额不足";
        }
    }

    // ---业务5:查全员---
    [HttpGet("All")]
    public object GetAllUsers()
    {
        // 直接把数据库表里的所有数据拿出来，转成List
        return _context.Accounts.Select( x => new
        {
            x.Id,
            x.AccountName,
            x.Balance,
            Type = x.GetType().Name // 看看是 Normal 还是 VIP
        }).ToList();
    }
}




