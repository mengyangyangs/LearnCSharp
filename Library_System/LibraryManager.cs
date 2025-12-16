// 图书馆管理员 (核心封装)
namespace LibrarySystem;

public class LibraryManager
{
    // 私有仓库：存所有的资产（Book，EBook，AudioBookCD 都在这里）
    private List<LibraryAsset> _assets = new List<LibraryAsset>();

    // 1.上架新书(多态参数)
    public void AddAsset(LibraryAsset asset)
    {   
        _assets.Add(asset);
        Console.WriteLine($"新资源入库:{asset.Title}");
    }

    // 2.搜索资源
    public void Search(string keyword)
    {
        Console.WriteLine($"\n 搜索结果:{keyword}");
        foreach (var item in _assets)
        {
            if (item.Title.Contains(keyword))
            {
                item.ShowDetails();
            }
        }
    }

    // 3.处理借阅(高难度逻辑)
    // 只有实现了 ILeasable 的东西才能被借！
    public void CheckoutItem(string title, string borrower, string userType)
    {
        // 先找书
        var item = _assets.Find( x => x.Title == title);
        if (item == null)
        {
            Console.WriteLine("查无此书");
            return;
        }
        // 关键判断:这玩意能借吗？
        // 例如EBook是不能借的，Book 和 CD是可以的
        if (item is ILeasable)
        {
            // 强转成接口，调用接口方法
            ILeasable leasableItem = (ILeasable)item;
            leasableItem.Borrow(borrower);
        }
        else
        {
            Console.WriteLine($"❌ 《{item.Title}》 是数字资源，不可外借，请直接在线访问。");
        }

    }

    // 4.归还物品
    public void ReturnItem(string title)
    {
        var item = _assets.Find(x => x.Title == title );
        if (item == null)
        {
            Console.WriteLine("归还失败，查无此书");
            return;
        }
        
        // 只有实现了 ILeasable 接口的物品才能归还
        if (item is ILeasable)
        {
            ILeasable leasableItem = (ILeasable)item;
            leasableItem.Return();
        }
        else
        {
            Console.WriteLine($"🚫 《{item.Title}》 是数字资源，无需归还。");
        }
    }
}

