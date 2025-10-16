using System;        
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace QuickCode.Demo.Common.Models
{
    public class Response<T>
    {
        public string Message { get; init; } = "No Message";

        public int Code { get; set; }

        public T Value { get; set; } = default!;
        
        public static Response<T> NotFound(string message = "Not found") => new()
        {
            Code = 404,
            Message = message,
            Value = default!
        };
        
        public static Response<T> BadRequest(string message = "Bad request") => new()
        {
            Code = 400,
            Message = message,
            Value = default!
        };
    }
}
