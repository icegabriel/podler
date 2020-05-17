using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.Responses
{
    public class AddComicResponse : IResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        public AddComicResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
