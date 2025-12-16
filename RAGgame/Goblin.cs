namespace RPGGame;

public class Goblin : IMonster
{
    public string Name {get;set;} = "邪恶的哥布林"; // 默认名字
    public int Hp {get;set;} = 100; // 默认血量100

    public void GetHit(int damage)
    {
        Hp -= damage;
        Console.WriteLine($"👻{Name}惨叫一声，掉了{damage}滴血，(剩余{Hp})");

        if (Hp <= 0)
        {
            Console.WriteLine($"💀{Name}倒地身亡！");
        }
    }

    public void Attack(Hero target)
    {
        Console.WriteLine($"👻{Name}挥舞着木棒，向{target.Name}发动了攻击！");
        target.TakeDamage(500); // 哥布林每次攻击造成50点伤
    }
}