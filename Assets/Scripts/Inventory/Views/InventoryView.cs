using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory.Views
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView[] _slots;

        public InventorySlotView GetInventorySlotView(int index)
        {
            return _slots[index];
        }
    }
}
