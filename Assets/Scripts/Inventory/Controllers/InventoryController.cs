using Inventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class InventoryController
    {
        private readonly List<InventorySlotController> _slots;

        public InventoryController(IReadOnlyInventoryGrid inventory, InventoryView view) 
        {
            var size = inventory.Size;
            var slots = inventory.GetSlots();

            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotView = view.GetInventorySlotView(index);
                    var slot = slots[i, j];
                    _slots.Add(new InventorySlotController(slot, slotView));
                }
            }
        }
    }
}
