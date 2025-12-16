namespace HRSystem;

public class Developer : Employee , IWorkReport
{
    // 特有属性
    public double BaseSalary {get;set;} // 基本工资
    public int OvertimeHours {get;set;} // 加班时长
    public double HourlyRate {get;set;} = 200; // 加班费每小时200

    // 构造函数
    public Developer(string name, double baseSalary , string department) : base(name,department)
    {
        BaseSalary = baseSalary;
        
    }

    // 重写：算工资
    public override double CalculateSalary()
    {
        return BaseSalary + (OvertimeHours * HourlyRate);
    }

    // 实现接口:交日报
    public void SubmitReport()
    {
        Console.WriteLine($"💻 程序员{Name}提交了代码提交记录(Git log).");
    }
}