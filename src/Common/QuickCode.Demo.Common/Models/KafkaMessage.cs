namespace QuickCode.Demo.Common.Models; 
public class KafkaMessage
{
    public RequestInfo? RequestInfo { get; set; }
    public ResponseInfo? ResponseInfo { get; set; }
    public string ExceptionMessage { get; set; } = null!;
    public int ElapsedMilliseconds { get; set; }
    public DateTime Timestamp { get; set; }
}