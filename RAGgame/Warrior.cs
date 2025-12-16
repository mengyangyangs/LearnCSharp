// 战士类，继承自Hero

namespace RPGGame;

public class Warrior : Hero
{
    // 构造函数：直接传给父类
    public Warrior(string name) : base(name,500) // 战士血厚，默认500
    {
        
    } 

    // 必须重写！否则报错
    // 战士的攻击方式：物理砍杀
    public override void Attack()
    {
        Console.WriteLine($"[战士]：{Name}挥舞着大砍刀，发动了旋风斩!（物理伤害）");
    }
}