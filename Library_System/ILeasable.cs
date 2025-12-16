// 可租赁的 - 针对实体物品
namespace LibrarySystem;

public interface ILeasable
{
    // 借出动作：需要知道是谁借的
    // 返回 bool：借出成功/失败

    bool Borrow(string borrowName);

    // 归还动作
    void Return();

    // 计算滞纳金：不同的人（userType）有不同的计算规则
    // userType：Student 或 Professor
    // daysLate：预期天数
    double CalculateFine(string userType, int daysLate);
    
}