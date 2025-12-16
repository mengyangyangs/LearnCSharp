// 图书馆资产 - 抽象父类
namespace LibrarySystem;

public abstract class LibraryAsset
{
    public string Title {get;set;}
    public string ISBN {get;private set;} // 只读，不能修改

    // 资产状态：true=在馆/可用 false=借出/不可用
    // protected set：只有子类能修改状态，外面不能直接改
    public bool IsAvailable {get; protected set;} = true;

    public LibraryAsset(string title, string isbn)
    {
        Title = title;
        ISBN = isbn;
    }

    // 抽象方法：显示详情
    public abstract void ShowDetails();
}