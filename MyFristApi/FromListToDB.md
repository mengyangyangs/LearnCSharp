# 从 List 内存存储迁移到 SQLite 数据库指南

本文档总结了 `MyFristApi` 项目如何从使用内存中的 `List` 存储数据，迁移到使用 Entity Framework Core (EF Core) 和 SQLite 数据库。这种模式适用于大多数 .NET Web API 项目的数据持久化改造。

## 核心转变概念

*   **List (内存)**: 数据保存在程序的内存（RAM）中。
    *   *缺点*: 程序重启（停止调试或服务器重启）后，数据全部丢失。无法进行复杂查询。
*   **Database (SQLite)**: 数据保存在硬盘上的文件（如 `bank.db`）中。
    *   *优点*: 数据持久保存。支持大量数据和复杂查询。

---

## 改造步骤详解

### 1. 引入“数据库管理员” (DbContext)

首先，我们需要一个类来充当数据库的管理员，它负责把你的 C# 对象（如 `NormalAccount`）转换成数据库里的表行。

**文件**: `Data/DataContext.cs` (新文件)

```csharp
using Microsoft.EntityFrameworkCore;
using MyFirstApi.Models;

namespace MyFirstApi.Data;

// 继承自 DbContext，这是 EF Core 提供的基类
public class DataContext : DbContext
{
    // 构造函数：接收配置选项（比如数据库文件名叫什么）传给父类
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // DbSet 代表一张“表”
    // 这里表示数据库里有一张表叫 Accounts，存放 NormalAccount 类型的数据
    public DbSet<NormalAccount> Accounts { get; set; }
    
    // 这里表示有一张表叫 Transactions，存放 TransactionRecord 类型的数据
    public DbSet<TransactionRecord> Transactions { get; set; }
}
```

### 2. 在程序启动时注册服务

我们需要告诉 .NET 程序：“请使用 SQLite，并且用上面的 `DataContext` 来管理。”

**文件**: `Program.cs`

**旧代码 (无)**:
*以前不需要这一步，因为 List 是直接写在 Controller 里的静态变量。*

**新代码**:
```csharp
using MyFirstApi.Data; // 引入命名空间
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ... 其他服务注册 ...

builder.Services.AddControllers();

// 👇 新增：注册数据库服务
builder.Services.AddDbContext<DataContext>(options =>
{
    // 指定使用 SQLite，并设置数据库文件名为 bank.db
    options.UseSqlite("Data Source=bank.db");
});
```

### 3. 改造控制器 (Controller)

这是变化最大的地方。我们需要把手动操作 `List` 的代码，改成调用 `_context` (数据库管理员) 的代码。

**文件**: `Controllers/BankController.cs`

#### A. 依赖注入 (获取管理员)

**旧代码 (List)**:
```csharp
// 静态列表，所有请求共享
private static List<IBankAccount> _accounts = new List<IBankAccount>();
```

**新代码 (Database)**:
```csharp
private readonly DataContext _context; // 声明管理员

// 构造函数注入：当 Controller 被创建时，系统会自动把 _context 送进来
public BankController(DataContext context)
{
    _context = context;
}
```

#### B. 查询数据 (Find vs FirstOrDefault)

**旧代码 (List)**:
```csharp
// 在内存列表中查找
var acc = _accounts.Find(x => x.AccountName == name);
```

**新代码 (Database)**:
```csharp
// 去 Accounts 表里查找
// FirstOrDefault: 找第一个匹配的，找不到返回 null
var acc = _context.Accounts.FirstOrDefault(x => x.AccountName == name);
```

#### C. 插入数据 (Add vs Add + SaveChanges)

**旧代码 (List)**:
```csharp
_accounts.Add(newAcc); // 加到列表里就完事了
```

**新代码 (Database)**:
```csharp
_context.Accounts.Add(newAcc); // 1. 先把数据添加到“待保存区”
_context.SaveChanges();        // 2. 必须调用这个！通过这一步，数据才会真正写入 bank.db 文件
```

#### D. 修改数据 (直接改 vs 改 + SaveChanges)

**旧代码 (List)**:
```csharp
acc.Deposit(amount); // 直接改了对象，内存里就变了
```

**新代码 (Database)**:
```csharp
acc.Deposit(amount); // 内存里的对象变了
_context.SaveChanges(); // 必须调用！告诉数据库：“刚才那个对象变了，请更新到硬盘”
```

#### E. 关联数据查询 (Include)

这是一个数据库特有的概念。当你查“账户”时，默认**不会**把它的“交易记录”也查出来（为了省流量）。如果你需要，必须显式说明。

**新代码**:
```csharp
var acc = _context.Accounts
        .Include(x => x.Transactions) // ⭐ 关键：告诉数据库把关联的流水也带出来
        .FirstOrDefault(x => x.AccountName == name);
```

---

## 总结：以后如何套用？

如果你在别的项目里也想这样改，只需要做三件事：

1.  **建模型**: 创建你的数据类（如 `Product`, `User`）。
2.  **建Context**: 创建一个继承自 `DbContext` 的类，把你的模型加进去作为 `DbSet`。
3.  **改Controller**:
    *   注入 `DbContext`。
    *   把 `List.Add` 改成 `_context.Add` + `SaveChanges`。
    *   把 `List.Find` 改成 `_context.Users.FirstOrDefault`。
    *   每次修改数据后，别忘了 `SaveChanges()`。
