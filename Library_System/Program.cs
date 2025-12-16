namespace LibrarySystem;

class Program
{
    static void Main(string [] args)
    {
        Console.WriteLine("📖 ==== 欢迎来到图书借阅系统 ====");

        // 一.图书馆开张
        LibraryManager manager = new LibraryManager();

        // 二.新书入库
        // 我们创建三种不同的资源，统统塞给AddAsset
        // 因为它们都继承自 LibraryAsset，所以都能塞进去

        // 1.实体书
        Book book1 = new Book("C#入门到放弃", "978-001");
        manager.AddAsset(book1);

        // 2.电子书（注意构造函数中的参数要对应上）
        EBook ebook1 = new EBook("云原生架构设计", "978-002", 15.5);
        manager.AddAsset(ebook1);

        // 3.有声书光盘 (这是那个双接口的高级货)
        AudioBookCD cd1 = new AudioBookCD("周杰伦专辑", "CD-888", 70);
        manager.AddAsset(cd1);

        // 看看库存
        manager.Search("");

        Console.WriteLine("--------------------🖕🖕🖕🖕🖕🖕-----------------------------");
        
        // 三.借阅大戏
        // 剧情1:学生小明想借实体书
        Console.WriteLine("\n[测试1] 学生小明尝试借阅《C#入门到放弃》：");
        manager.CheckoutItem("C#入门到放弃", "小明", "Student");

        // 剧情2：教授老王想借同一本书 (应该失败)
        Console.WriteLine("\n[测试2] 教授老王尝试借阅《C#入门到放弃》：");
        manager.CheckoutItem("C#入门到放弃", "老王", "Professor");

        // 剧情3：小红想借电子书 (应该提示不可借)
        Console.WriteLine("\n[测试3] 小红尝试借阅《云原生架构设计》：");
        manager.CheckoutItem("云原生架构设计", "小红", "Student");

        // 剧情4：李雷想借光盘 (应该成功，因为光盘实现了 ILeasable)
        Console.WriteLine("\n[测试4] 李雷尝试借阅光盘《周杰伦专辑》：");
        manager.CheckoutItem("周杰伦专辑", "李雷", "Student");

        Console.WriteLine("--------------------🖕🖕🖕🖕🖕🖕-----------------------------");

        // 四.归还与罚款
        Console.WriteLine("\n[测试5] 小明归还《C#入门到放弃》：");
        // 调用我们刚才补写的 ReturnItem 方法
        manager.ReturnItem("C#入门到放弃");

        // 演示一下罚款计算 (这里因为我们拿不到具体的 Book 对象，只能手动模拟演示一下多态逻辑)
        // 假设书逾期了3天
        Console.WriteLine($"\n[罚款计算] 假设这本书逾期了 3 天...");
        Console.WriteLine($"如果是学生(每天1元): 需缴纳 {book1.CalculateFine("Student", 3)} 元");
        Console.WriteLine($"如果是教授(免费): 需缴纳 {book1.CalculateFine("Professor", 3)} 元");

        // 演示光盘罚款 (光盘比较贵，每天5元)
        Console.WriteLine($"[罚款计算] 假设光盘逾期了 3 天...");
        Console.WriteLine($"学生需缴纳: {cd1.CalculateFine("Student", 3)} 元");
        Console.WriteLine($"教授需缴纳: {cd1.CalculateFine("Professor", 3)} 元");
        Console.WriteLine("\n=== 图书馆系统测试结束 ===");
    }
    
}