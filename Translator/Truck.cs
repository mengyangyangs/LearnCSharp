namespace LogisticsSystem;

// 既继承父类 Vehicle，又实现接口 IMaintainable
public class Truck : Vehicle // ,IMaintainable
{
    public Truck(string name, double oil) : base(name,oil)
    {
        // 什么也不做
    }

    // 重写运输逻辑
    public override double Transport(string item, int distance)
    {
        // 模拟逻辑：卡车比较省油，每次只耗油5%
        if (Oil < 5)
        {
            Console.WriteLine($"🚫 [警告] {Name} 油箱见底 (剩余 {Oil}%)，无法发车！");
            return 0; // 没运成，运费为 0
        }

        // 消耗油量(直接修改自己的属性)
        Oil = Oil - 5;

        // 卡车运费：距离*2.0
        double cost = distance * 2.0;
        Console.WriteLine($"🚛{Name}正在公路上行驶，运输:{item}...");
        Console.WriteLine($" 行驶{distance}公里，运费:{cost}元");
        return cost;
    }

    // // 实现接口方法：维护保养
    // public void Maintenance()
    // {
    //     Console.WriteLine($"🚛{Name}正在进行维护保养，检查发动机和轮胎...");
    // }

    // 重写维护保养方法
    public override bool Maintenance()
    {
        // 模拟逻辑：卡车比较厚实，只有油量低于20%，才需要保养
        if (Oil < 20)
        {
            Console.WriteLine($"🛢️ {Name} 正在加油站：更换机油和润滑剂...");
            
            // 保养动作：把油加满
            Oil = 100;

            Console.WriteLine($"   保养完成，油量已充满 (100%)！");
            return true; // 返回 true：我确实进行了保养
        }
        else
        {
            // 如果油还挺多，就不保养
            // ⭐ 必须写这个 else 或者是最后的 return false，防止报错
            Console.WriteLine($"✅ {Name} 状态良好 (油量 {Oil}%)，无需保养。");
            return false; // 返回 false：我没保养
        }
    }
}