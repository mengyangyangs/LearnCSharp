namespace MyFristApp;
using System.IO; // 引入文件操作的命名空间
class Program
{
    static void Main(string[] args)
    {
        Calculate();

    }
    static void Calculate()
    {
        // 语法： 类名 + 变量名 = new 类名()
        // 其实就是创建实例，Calculator类的实例 
        // Calculator myCalc = new Calculator("小马牌计算机")

        // 使用子类类型，方便调用新方法
        // ScientificCalculator myCalc = new ScientificCalculator("小马牌超级计算器Pro Max");

        // 以前我们限定死了只能用 Calculator
        // 现在我们声明变量类型是：ICalculator（接口）
        // 可以随时在这里切换硬件，代码逻辑不用变！

        // 选项A：用原来的计算器
        // ICalculator myCalc = new ScientificCalculator("小马牌超级计算器Pro Max");

        // 选项B：换成新买的手机，由于新买的手机没有计算幂的功能，所以如果要使用手机，请把下面的幂功能给注释掉
        ICalculator myCalc = new Phone("苹果13");

        // 直接调用这个“多态”方法
        // 虽然方法都叫做SayHello()，但实际调用的是子类的版本
        myCalc.SayHello();
        Console.WriteLine("当前设备:" + myCalc.Brand);
        
        // 语法：List<类型> 变量名 = new List<类型>()
        List<string> history = new List<string>();

        while (true)
        {
            Console.WriteLine("----------------------------");
            double number1 = 0;
            double number2 = 0;

            // 第一个数
            Console.WriteLine("请你输入第一个数:");
            try
            {
                string num1 = Console.ReadLine() ?? "0";
                if (num1 == "exit")
                {
                    return;
                }
            // 字符串转数字
                number1 = double.Parse(num1);
            }
            catch
            {
                Console.WriteLine("输入的第一个数格式不正确");
                continue;
            }
            
            // 选择计算类型
            Console.WriteLine("请选择你要计算的类型(+ - * / ^):");
            string op = Console.ReadLine() ?? "";

            try
            {
                // 第二个数
                Console.WriteLine("请你输入第二个数:");
                string num2 = Console.ReadLine() ?? "0";
                if (num2.ToLower() == "exit")
                {
                    return;
                }
                // 字符串转数字
                number2 = double.Parse(num2);
            }
            catch
            {
                Console.WriteLine("输入的第二个数格式不正确");
                continue;
            }

            double result = 0;

            switch (op) // 用switch结构代替了if-else结构
            {
                case "+":
                    result = myCalc.Add(number1,number2);
                break;

                case "-":            
                    result = myCalc.Sub(number1,number2);
                break;
            
                case "*":
                    result = myCalc.Mul(number1 , number2);
                break;

                case "/":
                    result = myCalc.Div(number1 , number2);
                break;

                // 当使用选项A时打开
                // case "^":
                //     result = myCalc.Power(number1,number2);
                //     break;
            
                default:
                    Console.WriteLine("不支持的运算符");
                continue;
            }
            // 打印结果
                Console.WriteLine("计算结果是:" + result);
            // 保存历史记录
                string record = number1 + " " + op + " " + number2 + " = " + result;
                history.Add(record);
            // 询问是否继续
                Console.WriteLine("是否还想要继续，如果不想请输入n退出,否则按任意键继续：");
                string answer = Console.ReadLine() ?? " ";
                if (answer.ToLower() == "n")
                {
                    break;
                }
        }
        Console.WriteLine("欢迎下次使用！");
        foreach (string record in history)
        {
            Console.WriteLine("计算历史记录：" + record);
        }
        File.WriteAllLines("calc_history.txt",history);
        Console.WriteLine("计算历史记录已保存为calc_history.txt文件，请查收！");

    }
}