// 实体书
namespace LibrarySystem;

public class Book : LibraryAsset,ILeasable
{
    public Book(string title, string isbn) : base(title,isbn)
    {
        
    }

    public override void ShowDetails()
    {
        Console.WriteLine($" [实体书]《{Title}》 (ISBN:{ISBN}) | 状态:{(IsAvailable ? "在架上":"已借出")} ");
    }

    // ---实现 ILeaseable
    public bool Borrow(string borrowName)
    {
        if (IsAvailable)
        {
            IsAvailable = false; // 修改父类状态,表示该图书已被借走
            Console.WriteLine($"📖 {borrowName} 成功借阅了《{Title}》");
            return true; // 表示成功借出
        }
        else
        {
            Console.WriteLine($"❌ 《{Title}》已被借走");
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
        // 教授逾期不用罚钱，学生超过一天罚1元
        if (userType == "Professor")
        { 
            return 0;
        }
        return daysLate * 1.0;
    }
}