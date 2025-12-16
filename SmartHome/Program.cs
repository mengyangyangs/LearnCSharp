namespace SmartHome;

class Program
{
    static void Main(string [] args)
    {

        Console.WriteLine("\n===================== WiWiWi 智能家居 =======================");

        List<SmartDevice> devices = new List<SmartDevice>();

        // 1.存放智能灯
        SmartLight light = new SmartLight("超绝氛围灯","卧室");
        devices.Add(light);

        // 2.存放空调
        AirConditioner conditioner = new AirConditioner("大美的","客厅");
        devices.Add(conditioner);

        // 3.存放报警器
        SmokeSensor sensor = new SmokeSensor("超人牌报警器","厨房");
        devices.Add(sensor);

        // 打开所有设备
        foreach (SmartDevice device in devices)
        {
            if (device is ISwitchable)
            {
                // 由于报警器没有使用接口，所以要打开设备需要先转换类型
                ((ISwitchable)device).TurnOn();
            }
            device.ShowStatus();

            // // 更简洁的写法:使用模式匹配，判断的同时直接转换
            // if (device is ISwitchable deviceSwitch)
            // {
            //     deviceSwitch.TurnOn();
            // }
        }

        Console.WriteLine("\n============================关闭所有设备，但报警器常开============================");
        // 当人离开后，除了报警器不关闭，别的都需要关闭
        foreach (SmartDevice device in devices)
        {
            if (device is ISwitchable)
            {
                // 由于报警器没有使用接口，所以要关闭设备需要先转换类型
                ((ISwitchable)device).TurnOff();
            }
            else
            {
                Console.WriteLine($"报警器 [{device.Name}] 必须保持全天候开启...");
            }
            device.ShowStatus();
        }

        // 测试1.当家里着火了，是否会报警
        sensor.DetectSmoke();
        sensor.ShowStatus();

        // 测试2.能否复位
        sensor.Reset();
    }
}