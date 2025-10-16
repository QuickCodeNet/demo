namespace QuickCode.Demo.Common.Models
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; } = default!;
        public List<string> DetailErrorCodes { get; set; } = new();
    }
}

