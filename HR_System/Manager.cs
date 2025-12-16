namespace HRSystem;

public class Manager : Employee , IWorkReport
{
    // 特有属性
    public double FixedSalary {get;set;} // 固定工资
    public double Bouns {get;set;} // 奖金

    // 构造函数
    public Manager(string name, double salary, double bouns, string department) : base(name,department)
    {
        FixedSalary = salary;
        Bouns = bouns;
    }

    public override double CalculateSalary()
    {
        return FixedSalary + Bouns;
    }

    public void SubmitReport()
    {
        Console.WriteLine($"🧑‍💼 经理{Name}提交了团队季度规划PPT");
    }
}