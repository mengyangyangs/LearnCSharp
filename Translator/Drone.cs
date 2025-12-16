namespace LogisticsSystem;

//  既继承父类 Vehicle，又实现接口 IMaintainable
public class Drone : Vehicle //,IMaintainable
{
    public Drone(string name , double oil) : base(name,oil)
    {
        
    }

    // 重写运输逻辑 
    // 使用内部属性 Oil，而不是参数
    public override double Transport(string item, int distance)
    {
        // 先检查油量
        if (Oil < 20)
        {
            Console.WriteLine($"⚠️ [警告] {Name} 油量仅剩 {Oil}%，不足以支撑飞行！无法起飞！");
            return 0; // 没运成，运费为 0
        }
        
        // 消耗油量(直接修改自己的属性)
        Oil = Oil - 20;

        // 无人机运费：距离 * 10.0
        double cost = distance * 10.0;
        Console.WriteLine($"✈️{Name}正在起飞！走直线运输{item}...");
        Console.WriteLine($" 飞行{distance}公里，运费:{cost}元");
        return cost;
    }

    // // 实现接口方法：维护保养
    // public void Maintenance()
    // {
    //     Console.WriteLine($"✈️{Name}正在进行维护保养，检查螺旋桨和电池...");
    // }

    // 重写维护保养方法
    public override bool Maintenance()
    {

        // 逻辑：如果油量低于 50，就进行保养（充电）
        // 注意：这里不需要传参数，直接读自己的 Oil 属性
        if (Oil < 50)
        {
            Console.WriteLine($"🔌 {Name} 正在充电站：更换高性能电池...");
            
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