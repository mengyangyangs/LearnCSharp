namespace RPGGame;

public class Archer : Hero
{
    // 构造函数：直接传给父类
    public Archer(string name) : base(name,300) //射手血量适中，默认300
    {
        
    }

    // 必须重写Attck方法，否则会报错
    public override void Attack()
    {
        Console.WriteLine($"[射手]:{Name}拉开弓弦，发射出一支穿云箭！（物理伤害）");
    }
}