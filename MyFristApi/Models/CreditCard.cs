using System.Text.Json.Serialization; // 引入这个是为了防止“循环引用“报错

namespace MyFirstApi.Models;

public class CreditCard
{
    // 1.主键(Primary Key)
    // 数据库里的唯一身份证号，由数据库自动生成(1,2,3...)
    public int Id {get;set;}

    // 2.卡号
    // 现实中卡号是唯一的，我们在代码逻辑里去控制
    public string CardNumber {get;set;}

    // 3.支付密码(简单模拟，六位密码)
    public string PIN {get;set;}

    // 4.信用额度(比如50000)
    public double CreditLimit {get;set;}

    // --- 建立关系(Foreign Key)
    // 物理外键：这张卡属于哪个User的ID
    // 数据库存的时候，就存这个数字(例如 AccountId = 1)
    public int AccountId {get;set;}

    // 导航属性：给程序员用的
    // 让我们通过card.Account直接访问那个User对象
    // [JsonIgnore]的作用：
    // 当Web API 返回这张卡的信息时，不要顺藤摸瓜把用户信息也带出来
    // 否则 用户里又有卡 卡里又有用户 会陷入死循环
    [JsonIgnore]
    public NormalAccount? Account {get;set;}
}