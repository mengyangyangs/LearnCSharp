namespace RPGGame;

// abstract关键字：表示这是一个“抽象类”
// 1.你不能直接 new Hero() 因为英雄只是一个概念，不存在一个纯粹的“英雄”，只有具体的战士或法师
// 2.它专门用来给别人做继承
public abstract class Hero
{
    // 属性:名字
    public string Name {get;set;}
    // 属性：血量
    public int Hp {get;set;}
    // 构造函数：初始化名字和血量
    public Hero(string name, int hp)
    {
        Name = name;
        Hp = hp;
    }

    // 核心知识点：抽象方法
    // 1.必须写在abstract类里
    // 2.没有大括号{}，只有分号; （因为父类不提供默认实现）
    // 3.意思：所有继承我的子类，必须强制实现Attack方法
    public abstract void Attack();

    // 普通方法：抽象类也可以写普通方法，子类直接继承使用
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        Console.WriteLine($"{Name}受到了{damage}点伤害，剩余血量为{Hp}");
        
        if (Hp <= 0)
        {
            Console.WriteLine($"{Name}倒地身亡！");
        }
    }
}