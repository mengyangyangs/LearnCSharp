# 🎓 C# 核心知识点与语法总结 (基于项目源码)

本文档基于当前目录下的 9 个实战项目，总结了其中涉及的核心 C# 编程知识点。这些笔记结合了代码中的实际应用场景，旨在帮助快速回顾语法和理解设计思想。

---

## 1. 类与对象 (Class & Objects)
**概念**: 类是图纸，对象是根据图纸造出来的实物。
**语法**:
```csharp
// 定义类
public class Car 
{
    // 构造函数 (名字与类名相同，new的时候自动执行)
    public Car(string name) { ... }
}

// 创建对象 (实例化)
Car myCar = new Car("特斯拉");
```
**项目案例**: 
- `Calculator`: `Calculator myCalc = new Calculator("小马牌");`
- `Product`: `VendingMachine vm = new VendingMachine();`

---

## 2. 封装 (Encapsulation) & 属性 (Properties)
**概念**: 保护数据不被外部随意修改，只暴露必要的接口。
**语法**:
```csharp
public class Person 
{
    // 自动属性：{ get; set; }
    // private set 表示外部只能读，不能改
    public string Name { get; private set; }
    
    // 只有本类和子类能修改 (protected)
    public int Age { get; protected set; }
}
```
**项目案例**:
- `Product/VendingMachine.cs`: `public double Balance { get; private set; }` (防止用户直接改余额，必须通过投币方法修改)。
- `Translator/Vehicle.cs`: `public double Oil { get; protected set; }` (油量只有载具自己或子类能消耗)。

---

## 3. 继承 (Inheritance)
**概念**: 子类复用父类的代码，并可以添加新功能。
**语法**:
```csharp
// : 后面跟父类名字
public class Dog : Animal 
{
    // base() 调用父类的构造函数
    public Dog(string name) : base(name) { }
}
```
**项目案例**:
- `Calculator`: `ScientificCalculator : Calculator` (科学计算器继承了普通计算器，拥有了所有基础功能，还加了幂运算)。
- `Library_System`: `Book`, `EBook` 都继承自 `LibraryAsset`。

---

## 4. 多态 (Polymorphism) —— 核心中的核心
**概念**: 同一个方法调用，根据对象实际类型的不同，表现出不同的行为。通常结合 `virtual` (虚方法) 和 `override` (重写) 使用。
**语法**:
```csharp
// 父类
public virtual void SayHello() { Console.WriteLine("我是父类"); }

// 子类
public override void SayHello() { Console.WriteLine("我是子类，我要改写逻辑"); }

// 使用
Parent p = new Child();
p.SayHello(); // 输出 "我是子类..."
```
**项目案例**:
- `Calculator`: `myCalc.SayHello()` 在普通计算器和科学计算器中有不同输出。
- `HR_System`: `e.CalculateSalary()` 遍历员工列表时，自动根据是 Developer 还是 Sales 调用不同的算薪公式。

---

## 5. 抽象类 (Abstract Class)
**概念**: 专门用来当“基座”的类，不能直接 `new`。它定义标准（抽象方法），强迫子类必须去实现。
**语法**:
```csharp
public abstract class Hero 
{
    // 抽象方法：没有方法体，只有分号
    public abstract void Attack(); 
}

// 子类必须实现
public class Warrior : Hero 
{
    public override void Attack() { ... }
}
```
**项目案例**:
- `RAGgame`: `Hero` 是抽象类，你不能创建一个单纯的“英雄”，必须创建 Warrior 或 Mages。
- `SmartHome`: `SmartDevice` 是抽象类，强制所有设备必须实现 `ShowStatus()`。

---

## 6. 接口 (Interface)
**概念**: 一份“契约”或“能力证明”。它只规定“能做什么”，不关心“怎么做”。一个类可以继承一个父类，但可以实现多个接口。
**语法**:
```csharp
// 定义接口 (通常以 I 开头)
public interface ISwitchable 
{
    void TurnOn();
}

// 实现接口
public class Light : ISwitchable 
{
    public void TurnOn() { Console.WriteLine("灯亮了"); }
}
```
**项目案例**:
- `IBankProject`: `IBankAccount` 规定了所有账户必须有 `Deposit` 和 `Withdraw` 方法。
- `Library_System`: `ILeasable` 接口用来标记哪些物品（如实体书）可以被借出，电子书没有实现该接口所以不能借。
- `Calculator`: `ICalculator` 接口让 `Phone` 和 `ScientificCalculator` 都能被当作计算器使用。

---

## 7. 类型判断与转换 (Type Casting & Pattern Matching)
**概念**: 在多态集合中，有时候需要还原对象的真实身份，或者判断它有没有某种能力（接口）。
**语法**:
```csharp
// 1. is 关键字判断
if (device is ISwitchable) 
{
    // 2. 强转
    ISwitchable d = (ISwitchable)device;
    d.TurnOn();
}

// 3. 模式匹配 (C# 新语法，更推荐)
if (device is ISwitchable d) 
{
    d.TurnOn();
}
```
**项目案例**:
- `SmartHome`: 遍历设备列表时，判断 `device is ISwitchable` 来决定是否能调用开关方法（报警器因为没实现该接口，所以不会被关闭）。
- `Library_System`: `if (item is ILeasable)` 判断资源是否可外借。

---

## 8. 集合 (Collections)
**概念**: 动态数组，比数组更灵活，可以随时 `Add` (增加) 或 `Find` (查找)。
**语法**:
```csharp
List<string> history = new List<string>();
history.Add("记录1");

// 查找 (Lambda表达式)
var item = list.Find(x => x.Name == "目标名字");
```
**项目案例**:
- `Product`: `_inventory` 列表存储所有商品。
- `MyFristApi`: `static List<IBankAccount> _accounts` 在内存中存储所有银行用户。

---

## 9. 异常处理 (Exception Handling)
**概念**: 防止程序因为错误输入而崩溃。
**语法**:
```csharp
try 
{
    int num = int.Parse("abc"); // 这行会报错
} 
catch 
{
    Console.WriteLine("格式错误！");
}
```
**项目案例**:
- `Calculator`: 解析用户输入的数字时，如果用户输入乱码，程序会提示错误而不是直接闪退。
- `IBankProject`: 存款金额转换时使用了 try-catch。

---

## 10. ASP.NET Core Web API 基础
**概念**: 构建可通过 HTTP 访问的服务。
**关键点**:
- **Controller**: 处理请求的大脑。
- **Attributes**: `[HttpGet]`, `[HttpPost]` 告诉程序这个方法对应哪个 URL 动作。
- **Stateless**: Web API 通常是无状态的，但我们的案例为了演示使用了 `static` 列表来模拟数据库。
**项目案例**:
- `MyFristApi`: `BankController.cs` 中的 `[HttpPost("OpenAccount")]` 对应开户接口。

---

## ✨ 总结
这 9 个项目循序渐进地构建了你的 C# 知识体系：
1. 从 **类与对象** 开始，学会造物。
2. 通过 **继承与多态**，学会代码复用和灵活扩展。
3. 利用 **抽象类与接口**，学会制定标准和解耦业务逻辑。
4. 结合 **集合与文件IO**，让数据动起来、存下来。
5. 最后迈向 **Web API**，让程序能够通过网络对外提供服务。

保持编码，继续加油！