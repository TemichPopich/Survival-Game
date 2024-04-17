using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    public interface IReadOnlyInventoryGrid : IReadOnlyInventory
    {
        event Action<Vector2Int> SizeChanged;

        Vector2Int Size { get; }

        IReadOnlyInventorySlot[,] GetSlots();
    }
}
