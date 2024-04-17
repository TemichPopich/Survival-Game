using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        public event Action<string> ItemChanged;
        public event Action<int> AmountChanged;

        private readonly SlotData _data;

        public string Item 
        { 
            get => _data._item; 
            set
            {
                if (_data._item != value)
                {
                    _data._item = value;
                    ItemChanged?.Invoke(value);
                }
            }
        }

        public int Amount 
        {
            get => _data._amount;
            set
            {
                if (_data._amount != value)
                {
                    _data._amount = value;
                    AmountChanged?.Invoke(value);
                }
            }
        }

        public bool IsEmpty => string.IsNullOrEmpty(Item) && Amount == 0;

        public InventorySlot(SlotData data) 
        { 
            _data = data; 
        }
    }
}
