namespace SmartHome;

// 只有“能开关”的设备才实现这个接口
public interface ISwitchable
{
    // 状态：开还是关？
    bool IsOn {get; }

    void TurnOn();
    void TurnOff();
}