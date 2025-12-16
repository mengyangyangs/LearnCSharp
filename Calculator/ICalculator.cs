namespace MyFristApp;

// 关键字是 interface （不是 class）
// 接口里不能写代码实现 （不能写大括号里的具体逻辑），只能写”目录“
public interface ICalculator
{
    // 属性契约
    string Brand {get;set;}

    // 方法契约：你必须会做这些事
    // 注意：没有public，也没有static，也没有大括号，直接分号结束
    void SayHello();
    double Add(double a,double b);
    double Sub(double a,double b);
    double Mul(double a,double b);
    double Div(double a,double b);
}