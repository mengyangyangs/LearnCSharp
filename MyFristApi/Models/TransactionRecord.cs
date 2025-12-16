using System;
using System.Data;
using System.Text.Json.Serialization; // 为了解决循环引用问题，后面会讲

namespace MyFirstApi.Models;

public class TransactionRecord
{
    public int Id {get;set;} // 流水号(主键)
    public double Amount {get;set;} // 交易金额(+100 或 -50)
    public double NewBalance {get;set;} // 交易后的余额
    public DateTime TransactionDate {get;set;} // 交易时间

    // --- 关键点：外键关系
    // 1.物理外键：记录这张单子属于哪个Account的Id
    public int AccountId {get;set;}

    // 2.导航属性：允许我们通过代码 record.Account 顺疼摸瓜找到主人
    // [JsonIgnore] 是为了防止 Web API 返回数据时死循环(人包含账单，账单包含人，人包含账单...)
    [JsonIgnore]
    public NormalAccount Account {get;set;}
}