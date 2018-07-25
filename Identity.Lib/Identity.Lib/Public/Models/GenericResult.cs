using System;

namespace Identity.Lib.Public.Models
{
    public sealed class GenericResult<TModel> where TModel : class 
    {
        public bool IsFailure { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public TModel Data { get; set; }
    }
}
