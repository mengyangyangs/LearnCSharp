namespace HRSystem;

public abstract class Employee
{
    // 知识点：静态字段
    // 它属于“Employee“类本身，所有员工共用这一个计算器
    // 每new一个员工，它就加1
    public static int _idCounter = 1000;

    // 属性
    public int Id {get;private set;} // 员工编号，外部只能看，不能改
    public string Name {get;set;}   // 员工名字
    public string Department {get;set;} // 员工部门

    // 构造函数 （构造函数的名字必须与类名相同）
    public Employee(string name , string department)
    {
        // 每次创建新员工，计数器加1，然后赋值给Id
        _idCounter++;
        Id = _idCounter;
        Name = name;
        Department = department;
    }

    // 抽象方法：计算工资
    // 因为每个岗位的算法规则不太一样，所以父类不写，只能让继承的子类去写
    public abstract double CalculateSalary();

    // 普通方法：展示员工信息
    public void ShowProfile()
    {
        Console.WriteLine($"[员工卡]工号:{Id} | 姓名:{Name} | 部门:{Department}" );
    }

    // 其实也可以把接口移到这里来
    // 然后在各个人员中，重写一遍各自的日报即可
    // public abstract void SubmitReport();
    
}