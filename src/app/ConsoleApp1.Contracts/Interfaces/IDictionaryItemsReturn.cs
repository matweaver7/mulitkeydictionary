using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Contracts.Interfaces
{
    public interface IDictionaryItemsReturn<k, v>
    {
        IEnumerable<k> keys { get; set; }
        IEnumerable<v> values { get; set; }
    }
}
