using Podler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.Responses
{
    public class AddCategoryResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public Category Category { get; }

        public AddCategoryResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public AddCategoryResponse(bool isSuccess, string menssage, Category category) : this(isSuccess, menssage)
        {
            Category = category;
        }
    }
}
