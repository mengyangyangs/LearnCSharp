// 有声书光盘
namespace LibrarySystem;

public class AudioBookCD : LibraryAsset,ILeasable,IDigital
{
    // ⭐ 修正2：必须把接口里规定的属性，显式地定义出来！
    // 不定义这个，电脑不知道 FileSizeMB 是谁
    public double FileSizeMB { get; private set; }
    public AudioBookCD(string title, string isbn , double filesizemb) : base(title,isbn)
    {   
        FileSizeMB = filesizemb;
    }
    public override void ShowDetails()
    {
        Console.WriteLine($" [有声书CD]《{Title}》(ISBN:{ISBN}) | Size:{FileSizeMB}MB | 状态:{(IsAvailable ? "可外借" : "已借出")}");
    }
    public bool Borrow(string borrowName)
    {
        if (IsAvailable)
        {
            IsAvailable = false; // 表示实体的书已经借走，不能看
            Console.WriteLine($"📖 {borrowName} 已经成功看到《{Title}》");
            return true;
        }
        else
        {
            Console.WriteLine("不能看");
            return false;
        }
    }
    public void Return()
    {
        IsAvailable = true;
        Console.WriteLine($" ✅《{Title}》已归还");
    }

    public double CalculateFine(string userType, int daysLate)
    {
        if (userType == "Professor")
        {
            return daysLate * 1.0;
        }
        else
        {
            return daysLate * 5.0;
        }
    }

    public void AccessContent()
    {
       Console.WriteLine($"🎧 正在播放试听片段：www.library.com/audio/{Title} ...");
    }
}