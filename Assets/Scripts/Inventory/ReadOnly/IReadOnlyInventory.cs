using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    public interface IReadOnlyInventory
    {
        event Action<string, int> ItemsAdded;
        event Action<string, int> ItemsRemoved;

        int GetAmount(string item);

        bool Has(string item, int amount);
    }
}
