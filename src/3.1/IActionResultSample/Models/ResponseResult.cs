using System;

namespace IActionResultSample.Models
{
    public abstract class ResponseBase
    {
        public string StatusCodes { get; set; }
        public virtual Object[] Results { get; set; }
    }

    public class ResponseResult : ResponseBase
    {
    }

    public class ResponseErrorResult : ResponseBase
    {
    }
}
