using Microsoft.AspNetCore.Mvc; // Web API的核心包
using MyFirstApi.Data; // 引用数据库配置
using MyFirstApi.Models; // 引用模型
using Microsoft.EntityFrameworkCore; //引用 EF Core 工具

namespace MyFirstApi.Controllers;

[ApiController] // 标记：这是一个控制器
[Route("[controller]")] // 路由规则：网页访问 /CreditCard 就能找到这
public class CreditCardController : ControllerBase
{
    // 定义一个私有的数据库管理员变量
    private readonly DataContext _context;

    // 构造函数：依赖注入
    // 这里的逻辑是：
    // 当程序启动时，Program.cs 里的 builder.Service 已经准备好了一个 DataContext
    // 当有人访问这个控制器时，系统会自动把那个准备好的 context 塞进来给我们用
    public CreditCardController(DataContext context)
    {
        _context = context;
    }

    // 动作：POST（用于创建数据）
    // 地址：/CreditCard/Apply
    [HttpPost("Apple")]
    public string ApplyForCard(string userName, string pin)
    {
        // 第一步：先检查人在不在
        // 逻辑：去 Accounts 表里查，看看有没有名字叫 userName 的人
        // FirstOrDefault：找到第一个就返回，找不到就返回null
        var user = _context.Accounts.FirstOrDefault(x => x.AccountName == userName);
        if (user == null) return "申请失败：找不到该用户！";

        // 第二步：创建一张新卡对象
        CreditCard newCard = new CreditCard
        {
            CardNumber = "8888-" + new Random().Next(1000,9999), // 模拟生成卡号
            PIN = pin,
            CreditLimit = 50000,

            // 关键点
            // 我们把这张卡挂在这个人名下
            Account = user
        };

        // 第三步：告诉管理员“我要加张卡“
        // 注意：这时候只是内存里标记了一下，还没存硬盘
        _context.CreditCards.Add(newCard);

        // 第四步：保存更改(Commit)
        // 这一步执行后，数据才会真正写入 bank.db
        _context.SaveChanges();

        return $"办卡成功！卡号:{newCard.CardNumber},额度:{newCard.CreditLimit}";
    }

    // 动作：GET（用于获取数据）
    // 地址：/CreditCard/MyCards
    [HttpGet("MyCards")]
    public object GetMyCards(string userName)
    {
        // 第一步：先找到人
        var user = _context.Accounts.FirstOrDefault(x => x.AccountName == userName);
        if (user == null) return "申请失败：找不到该用户！";

        // 第二步：去卡库里找属于这个人的卡
        // 逻辑：在 CreditCards 表里筛选(Where)，条件是 AccountId 等于这个人的 ID
        // ToList():把筛选结果打包成一个列表
        var cards = _context.CreditCards.Where(x => x.AccountId == user.Id).ToList();

        // 第三步：返回数据
        // 如果我们直接返回 cards ，因为里面有 PIN ，不安全
        // 所以我们用 Selcet 挑选(投影)出只有用的信息
        return cards.Select( c => new
        {
            Number = c.CardNumber,
            Limit = c.CreditLimit,
            Owner = user.AccountName // 顺便把主人的名字带上
        });
    }

    // 动作：DELETE(用于删除数据)
    // 地址：/CreditCard/Cancel
    [HttpDelete("Cancel")]
    public string CancelCard(string cardNumber)
    {
        // 第一步：先去数据库里找这张卡
        // 逻辑：找 CardNumber 等于输入值的卡
        var card = _context.CreditCards.FirstOrDefault(x => x.CardNumber == cardNumber);

        // 如果没找到
        if (card == null) return "注销失败，卡号不存在！";

        // 第二步：告诉管理员“把这个东西删了”
        // Remove 只是在小本本上记一笔“要删除”，还没真删
        _context.CreditCards.Remove(card);

        // 第三步：保存更改
        // 这时候，数据库里那一行数据就彻底删除了
        _context.SaveChanges();

        return $"卡片:{cardNumber}已成功注销";
    }

}