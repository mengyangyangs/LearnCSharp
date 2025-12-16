// 电子书
namespace LibrarySystem;

public class EBook : LibraryAsset, IDigital
{

    // 修正1：实现接口规定的属性
    // 接口说要有 FileSizeMB，你就必须在这里把它声明出来
    public double FileSizeMB {get; private set;}

    // 修正2:构造函数参数补全
    // 父类需要ISBN，所以我们这里也要接受isbn，并用base(title,isbn)传给父亲
    public EBook(string title, string isbn, double filesizeMB) : base(title,isbn)
    {
        FileSizeMB = filesizeMB;
    }

    public override void ShowDetails()
    {
        Console.WriteLine($" [电子书]《{Title}》(ISBN:{ISBN}) Size:{FileSizeMB}MB | 状态:在线访问");
    }

    public void AccessContent()
    {
        Console.WriteLine($"正在打开链接：www.library.com/ebook/{Title} ...");
    }
}