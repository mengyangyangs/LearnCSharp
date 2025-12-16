namespace MyFristApp;
using System.IO; // 引入文件操作工具包

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===👏欢迎来到银行系统🏦===");

        // ---1.创建账户（接口的使用）---
        // 知识点复习：依赖接口
        // 我们声明变量类型为IBankAccount（合同），具体实例化哪个类（Normal或VIP）都可以。

        IBankAccount myAccount; //先声明一个假盒子 

        Console.WriteLine("请选择账户类型：1.普通账号，2.VIP账号(存钱送10%)");
        string type = Console.ReadLine() ?? "1";

        Console.WriteLine("请输入您的姓名：");
        string name = Console.ReadLine() ?? "无名氏";

        if (type == "2")
        {
            myAccount = new VIPAccount(name,100);
        }
        else
        {
            myAccount = new NormalAccount(name,100);
        }

        Console.WriteLine($"开户成功!名字为:{myAccount.Accountname}。当前余额为:{myAccount.Balance}");

        // ---2.准备交易笔记本---
        // 知识点复习：List集合
        // 用来动态存储每一句话，相当于银行流水
        List<string> logs = new List<string>();

        // 先记一笔开户记录
        logs.Add($"{DateTime.Now}:开户成功，名字为:{myAccount.Accountname},初始金额{myAccount.Balance}");

        // ---3.业务循环---
        while (true)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("请选择业务：1.存款，2.取卡，3.查询余额，4.退出（并打印结果）");

            string op = Console.ReadLine() ?? "0";

            // 知识点复习：Switch方法
            switch (op)
            {
                case "1": // 存款
                    Console.WriteLine("请输入存款金额：");
                    string inputIn = Console.ReadLine() ?? "0";

                    // 知识点复习：Try-catch
                    try
                    {
                        double moneyIn = double.Parse(inputIn);

                        // 调用接口方法：Deposit
                        // 如果是VIP，会自动触发VIP的逻辑（多态）
                        myAccount.Deposit(moneyIn);

                        // 记账
                        logs.Add($"{DateTime.Now}:存入{moneyIn},余额{myAccount.Balance}");
                    }
                    catch
                    {
                        Console.WriteLine("金额格式错误，请输入数字!");
                    }
                    break;

                case "2": //取款
                    Console.WriteLine("请输入取款金额:");
                    string inputOut = Console.ReadLine() ?? "0";

                    try
                    {
                        double moneyOut = double.Parse(inputOut);

                        // 调用 Withdraw,它会返回true或false
                        bool isSuccess = myAccount.Withdraw(moneyOut);

                        if (isSuccess)
                        {
                            logs.Add($"{DateTime.Now}:取出{moneyOut},余额{myAccount.Balance}元");
                        }
                        else
                        {
                            logs.Add($"{DateTime.Now}:取款失败({moneyOut},余额不足)");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("金额格式错误");
                    }
                    break;
                
                case "3": // 查询
                    // 这里用到接口属性Balance
                    Console.WriteLine($"当前余额为:{myAccount.Balance}");
                    break;

                case "4": //退出
                    Console.WriteLine("系统正在关闭，正在导出账单...");

                    // 知识点复习：File I/O（文件写入）
                    // 1.定义文件名
                    string fileName = "BankStatement.txt";
                    // 2.一次性把logs列表中的所有文字写入文件
                    File.WriteAllLines(fileName,logs);

                    Console.WriteLine($"账单已保存至{fileName},欢迎下次光临!");
                    return;

                default:
                    Console.WriteLine("无效指令，请重新输入");
                    break;
            }
        }
    }
}