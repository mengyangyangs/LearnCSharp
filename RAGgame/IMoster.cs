namespace RPGGame;

public interface IMonster
{
    // 怪物的名字
    public string Name {get;set;}

    // 怪物的血量
    public int Hp {get;set;}

    // 怪物必须能挨打
    void GetHit(int damage);

    // 怪物也能攻击英雄 (为什么是Hero类，因为所有的英雄都继承自Hero，如果单独写Warrior的话，那么Mages就不会被攻击)
    void Attack(Hero target);
}