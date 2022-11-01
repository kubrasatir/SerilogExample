using System;

namespace SerilogExample.Models
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
