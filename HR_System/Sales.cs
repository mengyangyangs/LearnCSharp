namespace HRSystem;

public class Sales : Employee , IWorkReport
{
    // 特有属性
    public double BaseSalary {get;set;} // 基本工资
    public double SalesAmount {get;set;} // 本月卖了多少钱

    // 构造函数
    public Sales(string name, double baseSalary, string department) : base(name,department)
    {
        // 提成5%
        BaseSalary = baseSalary;
    }

    public override double CalculateSalary()
    {
        return BaseSalary + (SalesAmount * 0.05);
    }

    public void SubmitReport()
    {
        Console.WriteLine($" 🧾销售{Name}提交了客户拜访记录表。");
    }
}