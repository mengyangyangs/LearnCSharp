// 数字化的 - 针对电子资源
namespace LibrarySystem;

public interface IDigital
{
    // 文件大小（MB）
    double FileSizeMB {get;}

    // 下载连接/在线阅读
    void AccessContent();
}