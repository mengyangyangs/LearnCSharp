namespace LogisticsSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 🎁顺丰物流调度系统启动 ===");

        // ---1.组件混合车队（List + 多态）
        // 这里的类型我们用 Vehicle ，因为它们都是载具
        List <Vehicle> fleet = new List<Vehicle>();

        fleet.Add(new Truck("擎天柱号重卡",100));
        fleet.Add(new Drone("御风者无人机",10));
        fleet.Add(new Truck("解放牌货车",15));

        // ---2.接到订单：开始送货
        Console.WriteLine("\n --- 🔔 接到加急订单：开始配送 ---");

        double totalIncome = 0; // 总收入

        foreach (Vehicle v in fleet)
        {
            // [多态演示1]
            // v.Transport():
            // 如果是卡车，就算便宜的钱。如果是无人机，就算贵的钱
            // 这里的100 是假设运输距离为 100 公里
            double fee = v.Transport("产品为:IPhone 17",100);

            // 逻辑小优化：如果运费是0，说明没运成(油不够)，不应该加进总收入
            if (fee > 0)
            {
                totalIncome += fee;
            }
            Console.WriteLine("------------------------------");
        }
        Console.WriteLine($"\n 本次辛苦配送一共赚了{totalIncome}元");

        // ---3.收工保养（接口的使用）
        Console.WriteLine("\n --- 🔧 订单完成，开始对车队进行维护保养 ---");

        foreach (Vehicle v in fleet)
        {
            // 难点来了：类型转换
            // v 目前被看作 Vehicle类，但Vehicle只有Transport方法，没有Maintenance方法
            // Maintenance 是 IMaintainable 接口里的

            // 我们要检查：这个载具 v，是不是遵守了保养合同？
            // if (v is IMaintainable)
            // {
            //     IMaintainable m = (IMaintainable)v; // 类型转换 把v变成IMaintainable类型
            //     m.Maintenance();
            // }

            // 直接调用重写后的维护保养方法
            bool m = v.Maintenance(); 
            if (m)  
            {
                Console.WriteLine($"{v.Name}保养完成，状态良好！");
            }
            else
            {
                Console.WriteLine($"👌 {v.Name} 状态良好 (油量充足)，无需保养。");
            }
        }
        Console.WriteLine("\n === 顺丰物流调度系统 运行结束 ===");
    } 
}