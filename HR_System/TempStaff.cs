namespace HRSystem;

public class TempStaff : Employee,IWorkReport
{
    // 特有属性：由于外包人员和正式工的工资构成不一样
    // 外包人员：基本工资 
    public double BaseSalary {get;set;} // 基本工资

    public TempStaff(string name, double baseSalary,string department) : base(name, department)
    {
        BaseSalary = baseSalary;
    }

    public override double CalculateSalary()
    {
        return BaseSalary;
    }

    public void SubmitReport()
    {
        Console.WriteLine($" 外包人员{Name}今日完成了工作");
    }

}