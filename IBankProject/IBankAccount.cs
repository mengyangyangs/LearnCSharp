namespace MyFristApp;
// 【知识点复习】：Interface (接口)
// 1. 接口名通常以 I 开头。
// 2. 接口是一份“契约”，只规定“做什么”，不写“怎么做”（没有大括号逻辑）。
// 3. 任何实现这个接口的类，都必须补全这些方法。

public interface IBankAccount
{
    // 属性契约:账户必须有个名字
    string Accountname {get;set;}

    // 属性契约:账户必须有余额
    // 只写 get 表示外部只能“看余额”(只能获取，不能写入)，不能直接“改余额”
    double Balance {get;}

    // 方法契约：存钱
    // 参数 amount：存入金额
    void Deposit(double amount);

    // 方法契约：取钱
    // return bools：取钱成功返回 true，余额不足返回 false
    bool Withdraw(double amount);
}