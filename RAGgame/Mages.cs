// 法师类

namespace RPGGame;
public class Mages : Hero
{
    // 构造函数：直接传给父类
    public Mages(string name) : base(name,200) // 法师血量较少，默认200
    {
        
    }
    
    // 必须重写
    // 法师的攻击方式：魔法轰炸！！！
    public override void Attack()
    {
        Console.WriteLine($"[法师]：{Name}吟唱了咒语，释放了[爆裂火球]！（魔法伤害）");
    }
}