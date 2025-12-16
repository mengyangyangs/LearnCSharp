# C# Learning Journey

这是一个 C# 学习项目的集合仓库，包含了从基础控制台应用到 Web API 的多个练习项目，涵盖了面向对象编程 (OOP)、接口、数据库交互等核心概念。

## 📂 项目目录结构

本仓库包含以下主要模块：

### 1. 基础与工具类
- **Calculator/**: 一个控制台计算器应用。
  - 包含基础运算与科学计算功能 (`ScientificCalculator.cs`)。
  - 实现了计算历史记录功能 (`calc_history.txt`)。
- **Product/**: 自动售货机模拟系统 (`VendingMachine.cs`)，用于练习状态管理和逻辑判断。

### 2. 面向对象编程 (OOP) 实践
- **HR_System/**: 人力资源管理系统。
  - 演示了继承与多态：`Employee` (基类) 派生出 `Manager`, `Developer`, `Sales`, `TempStaff`。
  - 包含接口实现 `IWorkReport`。
- **Library_System/**: 图书馆资产管理系统。
  - 涉及数字资产 (`IDigital`) 与可借阅资产 (`ILeasable`) 的接口设计。
  - 管理图书、电子书、有声读物等多种资源。
- **SmartHome/**: 智能家居模拟。
  - 通过 `ISwitchable` 接口统一管理设备（空调、智能灯、烟雾传感器等）。
- **Translator/**: 交通工具模拟系统。
  - 包含 `Drone` (无人机), `Truck` (卡车) 等，实现了 `IMaintainable` 接口。

### 3. 游戏与模拟
- **RAGgame/**: 一个基于文本的角色扮演游戏 (RPG) 雏形。
  - 包含角色系统：`Hero` (英雄), `Warrior`, `Archer`, `Mages`。
  - 包含怪物系统：`Goblin` 等，实现了 `IMoster` 接口。

### 4. 银行与金融系统
- **IBankProject/**: 银行账户管理系统的基础实现。
  - 区分 `NormalAccount` 和 `VIPAccount`。
  - 演示了接口 `IBankAccount` 的使用。

### 5. Web API 开发
- **MyFristApi/**: 一个完整的 ASP.NET Core Web API 项目。
  - **技术栈**: Entity Framework Core, SQLite (`bank.db`).
  - **功能**: 
    - 银行账户管理 (`BankController`)。
    - 信用卡管理 (`CreditCardController`)。
    - 数据库迁移记录 (`Migrations/`)。
    - 包含 HTTP 测试文件 (`MyFristApi.http`)。

## 📚 文档资源

- `project.md`: 原项目说明文件。
- `新项目开发指南.md`: 开发规范与向导。
- `base-knowlegde.md`: C# 基础知识笔记。
- `rule.md`: 项目规则说明。
- `project_summary.md`: 项目总结。

## 🚀 如何运行

大部分子文件夹为独立的控制台项目或 Web API 项目。
请进入相应目录并使用 dotnet CLI 运行：

```bash
# 运行控制台程序 (例如 Calculator)
cd Calculator
dotnet run

# 运行 Web API
cd MyFristApi
dotnet run
```