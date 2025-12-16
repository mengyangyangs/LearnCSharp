namespace RPGGame;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("====欢迎来到RPG游戏====");

        // ---1.组成战队（多态集合）
        // List<Hero> 既能装战士，又能装法师
        List<Hero> party = new List<Hero>();

        party.Add(new Warrior("亚瑟"));
        party.Add(new Mages("安哥拉"));
        party.Add(new Warrior("项羽"));
        party.Add(new Archer("后裔"));

        // ---2.刷新怪物
        // 使用接口类 IMoster
        IMonster enemy = new Goblin();
        Console.WriteLine($"野外出现了一只：{enemy.Name} (HP:{enemy.Hp})");
        Console.WriteLine("--------------------------------");

        // ---3.战斗开始（Foreach 循环）
        Console.WriteLine("战斗开始！全军出击！");

        foreach (Hero h in party)
        {
            // 多态关键时刻
            // h.Attack() 这一行代码：
            // 如果 h 是战士，就会打印旋风斩
            // 如果 h 是法师，就会打印火球术
            // 我们不需要写 if (h is Warrior) 之类的笨代码
            // h.Attack();
            
            // 判断英雄是否阵亡
            if (h.Hp <= 0)
            {
                Console.WriteLine($"英雄:{h.Name}已经阵亡,无法行动.");
                continue;
            }

            // 假设每次攻击都造成xx点伤害
            h.Attack();
            enemy.GetHit(50);

            // 判断怪物是否死亡
            if (enemy.Hp <= 0)
            {
                Console.WriteLine("怪物已经被消灭，战斗结束!");
                break; // 结束循环
            }

            Console.WriteLine("===============================");

            // 怪物反击
            Console.WriteLine("怪兽👾发起了进攻，它开始反击了！！！！");
            enemy.Attack(h);

            // 判断英雄是否阵亡
            if (h.Hp <= 0)
            {
                Console.WriteLine($"🚫 悲报：英雄 {h.Name} 在反击中牺牲了...");
            }
            
            Console.WriteLine("--------------------------------");
        }

        Console.WriteLine("战斗结束，感谢使用！");

    }
}