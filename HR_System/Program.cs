namespace HRSystem;

class Program
{
    static void Main(string [] args)
    {
        Console.WriteLine(" === 💰 科技公司HR 薪资管理系统 ===");

        // ---1.办理入职（自动生成工号演示）
        List<Employee> staffList = new List<Employee>();

        // 招一个程序员，底薪10000
        Developer dev = new Developer("小帅",10000,"软件部");
        dev.OvertimeHours = 20; // 加了20个小时的班
        staffList.Add(dev);

        // 招一个销售，底薪5000
        Sales sa = new Sales("小美",5000,"销售部");
        sa.SalesAmount = 100000;
        staffList.Add(sa);

        // 招一个经理，月薪30000，奖金5000
        Manager mag = new Manager("Jerry",30000,5000,"软件部");
        staffList.Add(mag);

        // 招了一个外包小伙，工资为8000
        TempStaff temp = new TempStaff("七七",8000,"劳务");
        staffList.Add(temp);

        // ---2.月底核算（多态）
        Console.WriteLine("\n ---- 月底发薪核算 ----");

        double totalPayout = 0; // 公司总支出

        foreach (Employee e in staffList)
        {
            // 打印名片（父类方法）
            e.ShowProfile();

            // 多态计算工资（关键点！）
            // 程序会自动判断是程序员还是销售
            double salary = e.CalculateSalary();

            Console.WriteLine($"  实发工资:{salary}元");
            totalPayout = totalPayout + salary; // 公司支付的薪水

            // 接口调用：检查日志
            if (e is IWorkReport)
            {
                // 类型转换并调用接口方法
                // 如果把接口调用的方法，写入到Employee中，就无需使用类型转换了
                // 因为 e 的类型是 Employee，要先把它转换成IWorkReport类型后，才能调用IWorkReport中的SubmitReport接口方法
                ((IWorkReport)e).SubmitReport();
            }
            Console.WriteLine("-----------------------------------------");
        }
        Console.WriteLine($"\n -------本月公司总人力支出:{totalPayout}元");
    }
}