using Newtonsoft.Json;
using System;

namespace SerilogExample.Models
{
    public class Response : ResponseBase
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public Response(string message = null)
        {
            Message = message;
        }
        public Response(string message = null, Exception exception = null)
        {
            Message = message;
            Exception = exception;
        }
        public Response(object data = null, string message = null)
        {
            Data = data;
            Message = message;
        }

        public Response(object data = null, string message = null, Exception exception = null)
        {
            Data = data;
            Message = message;
            Exception = exception;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
