namespace LogisticsSystem;
// 1.abstract 类：不能直接 new Vehicle(),只能被继承
public abstract class Vehicle
{
    // 属性：载具的名字（例如：东风重卡或大疆无人机）
    public string Name {get;set;}

    // 新增属性：油量是载具自己的属性，外部只能看(get)，只有子类能改(protected set)
    public double Oil {get; protected set; }

    // 构造函数：强制要求起名字
    public Vehicle(string name, double oil)
    {
        Name = name;
        Oil = oil;
    }

    // 抽象方法：运输
    // 我们不知道具体怎么运，也不知道多少钱，所以只定义“样子”
    // distance：距离（公里）
    // 返回值：这一单的运费
    // 方法不需要再传油量参数了，因为载具自己知道自己有多少油
    public abstract double Transport(string item, int distance);
    
    // 普通方法：展示信息
    public void ShowInfo()
    {
        Console.WriteLine($"载具名字:{Name}");
    }

    // 新增接口方法：维护保养 (true表示保养合格，false表示需要维修)
    // 保养也不需要传油量，自己检查自己的属性即可
    public abstract bool Maintenance();
}