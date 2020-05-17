using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.Responses
{
    public interface IResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
