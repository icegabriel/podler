using Podler.Models;
using Podler.Models.Interfaces;

namespace Podler.Responses
{
    public class SelectAddResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public ISelectableItem Model { get; }

        public SelectAddResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public SelectAddResponse(bool isSuccess, string menssage, ISelectableItem model) : this(isSuccess, menssage)
        {
            Model = model;
        }
    }
}
