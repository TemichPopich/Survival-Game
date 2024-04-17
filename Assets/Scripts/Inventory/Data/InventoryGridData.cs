using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    [SerializeField]
    public class InventoryGridData
    {
        public Vector2Int _size;
        public List<SlotData> _slots;
    }
}
