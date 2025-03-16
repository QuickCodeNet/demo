namespace QuickCode.Demo.Common.Models
{
    public class RepoResponse<T>
    {
        public RepoResponse() : this(default(T)!, "Success")
        {
        }

        public RepoResponse(T value, string message = "Success")
        {
            Code = 0;
            Message = message;
            Value = value;
        }

        public string Message { get; set; }
        public int Code { get; set; }
        public T Value { get; set; }
    }
}