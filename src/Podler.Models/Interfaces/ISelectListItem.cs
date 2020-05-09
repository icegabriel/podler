using System;
using System.Collections.Generic;
using System.Text;

namespace Podler.Models.Interfaces
{
    public interface ISelectListItem
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
