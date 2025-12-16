namespace VendingSystem;

public class VendingMachine
{
    // 核心知识点：封装
    // private：私有的，意味着Program.cs，根本看不到这个列表
    // 只有售货机内部的代码能动它。防止外面的人“偷饮料”
    private List<Product> _inventory = new List<Product>();

    // 余额：也只能看，不能改。必须通过投币来改
    public double Balance { get; private set;}

    // 构造函数:机器启动时，自动装填一些货物
    public VendingMachine()
    {
        _inventory.Add(new Product("可乐",3.0,5));
        _inventory.Add(new Product("红牛",6.0,0));
        _inventory.Add(new Product("矿泉水",2.0,4));
    }

    // 动作1:展示商品
    public void ShowMenu()
    {
        Console.WriteLine("--- 🍺 商品列表 ---");
        foreach (var v in _inventory) // var 自动推断类型
        {
            Console.WriteLine($"{v.Name} - 价格: {v.Price}元 - 库存: {v.Stock}");
        }
        Console.WriteLine("------------------");
    }

    // 动作2:投币
    public void InsertMoney(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("投币金额必须大于0");
            return;
        }
        Balance += amount;
        Console.WriteLine($"已投币：{amount}元，当前余额：{Balance}元");
    }

    // 动作3:购买
    // 返回 bool：告诉外面买没买成功
    public bool Purchase(string productName)
    {
        // 1.先找有没有这个货
        // Find 是 List的查找方法，找不到会返回 null
        Product p = _inventory.Find( x => x.Name == productName);

        // 2.检查货物是否存在
        if (p == null)
        {
            Console.WriteLine("❌ 商品不存在");
            return false;
        }

        // 2.5 检查库存
        if (p.Stock <= 0)
        {
            Console.WriteLine("❌ 商品缺货");
            return false;
        }

        // 3.检查钱够不够
        if (Balance < p.Price)
        {
            Console.WriteLine($"❌ 余额不足！商品需要:{p.Price}元，当前余额:{Balance}元");
            return false;
        }

        // 4.扣钱，出货
        Balance -= p.Price;
        p.Stock -= 1;
        Console.WriteLine($"✅ 购买成功！吐出商品:{p.Name}");
        Console.WriteLine($" 找零/剩余余额:{Balance}元");
        return true;
    }
}