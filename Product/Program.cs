namespace VendingSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" =========== 🤖 欢迎使用售货机 ==========");

        // 创建一台机器
        VendingMachine vm = new VendingMachine();

        // 看看有什么卖的
        vm.ShowMenu();

        // --- 模拟用户行为

        // 1.没投币直接购买(应该失败)
        Console.WriteLine("\n[测试1] 没给钱直接抢：");
        vm.Purchase("红牛");

        // 2.投币
        Console.WriteLine("\n[测试2] 投币买饮料：");
        vm.InsertMoney(5.0);

        // 3.买红牛(6元，钱不够，应该失败)
        Console.WriteLine("\n[测试3] 钱不够买红牛");
        vm.Purchase("红牛");

        // 4.再投2元(共7元)
        Console.WriteLine("\n [测试4] 再投2元");
        vm.InsertMoney(2.0);

        // 5.再次买红牛(应该成功)
        Console.WriteLine("\n[测试5] 钱够了，买红牛");
        vm.Purchase("红牛");
        // 购买成功后，更新库存
        vm.ShowMenu();

        // 6.用剩下的钱买水（1元，应该不够，水是2元）
        Console.WriteLine("\n[测试6] 用剩下的钱买水");
        vm.Purchase("矿泉水");


    }
}