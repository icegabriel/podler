using System;
using System.Collections.Generic;
using System.Text;

namespace Podler.Models.Interfaces
{
    public interface ISelectableItem
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
