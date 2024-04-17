using Inventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    internal class InventorySlotController
    {
        private readonly InventorySlotView _slotView;

        public InventorySlotController(
            IReadOnlyInventorySlot slot, 
            InventorySlotView slotView) 
        {
            _slotView = slotView;

            slot.ItemChanged += OnSlotItemChanged;
            slot.AmountChanged += OnSlotAmountChanged;

            slotView.ItemName = slot.Item;
            slotView.ItemAmount = slot.Amount;
        }

        private void OnSlotAmountChanged(int amount)
        {
            _slotView.ItemAmount = amount;  
        }

        private void OnSlotItemChanged(string itemName)
        {
            _slotView.ItemName = itemName;
        }
    } //meme4nto pattern | Observer | Singlton | Facade | Decorator | State Machine | Strategy | Decorator | Fabric | Builder zenjet
}
