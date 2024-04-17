using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    public interface IReadOnlyInventorySlot
    {
        event Action<string> ItemChanged;
        event Action<int> AmountChanged;

        string Item { get; }
        int Amount { get; }
        bool IsEmpty { get; }
    }
}
