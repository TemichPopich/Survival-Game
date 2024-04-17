using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using UnityEngine;

namespace Inventory
{
    internal class InventoryGrid : IReadOnlyInventoryGrid
    {
        public event Action<Vector2Int> SizeChanged;
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;

        private InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public Vector2Int Size 
        {
            get => _data._size;
            set
            {
                if (_data._size != value)
                {
                    _data._size = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }

        private int GetItemSlotCapacity(string item)
        {
            return 99;
        }

        private int AddToSameItemSlot(string item, int amount, out int remaining)
        {
            var itemsAddedAmount = 0;
            remaining = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var slot = _slotsMap[new Vector2Int(i, j)];

                    if (slot.IsEmpty) continue;

                    var slotItemCapacity = GetItemSlotCapacity(slot.Item);
                    if (slotItemCapacity <= slot.Amount) continue;

                    if (slot.Item != item) continue;

                    var newAmount = slot.Amount + remaining;

                    if (newAmount > slotItemCapacity)
                    {
                        remaining = newAmount - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remaining == 0) return itemsAddedAmount;
                    }
                    else
                    {
                        itemsAddedAmount = remaining;
                        slot.Amount = newAmount;
                        remaining = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int AddToAvailableSlot(string item, int amount, out int remaining)
        {
            var itemsAddedAmount = 0;
            remaining = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var slot = _slotsMap[new Vector2Int(i, j)];

                    if (!slot.IsEmpty) continue;

                    slot.Item = item;
                    var newAmount = remaining;
                    var slotItemCapacity = GetItemSlotCapacity(item);

                    if (newAmount > slotItemCapacity)
                    {
                        remaining = newAmount - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                    }
                    else
                    {
                        itemsAddedAmount += remaining;
                        slot.Amount = newAmount;
                        remaining = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        public AddToIventoryResult AddItems(Vector2Int coordinates, string item, int amount = 1)
        {
            var slot = _slotsMap[coordinates];
            var newAmount = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty) slot.Item = item;

            var itemSlotCapacity = GetItemSlotCapacity(item);

            if (newAmount > itemSlotCapacity)
            {
                var remaining = newAmount - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(item, remaining);
                itemsAddedAmount += result._itemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newAmount;
            }

            return new AddToIventoryResult(amount, itemsAddedAmount);
        }

        public AddToIventoryResult AddItems(string item, int amount = 1)
        {
            var remaining = amount;
            var itemsAddedToSameItemSlot = AddToSameItemSlot(item, amount, out remaining);

            if (remaining <= 0) return new AddToIventoryResult(amount, itemsAddedToSameItemSlot);

            var itemsAddedToAvailableSlotAmount = AddToAvailableSlot(item, amount, out remaining);
            var totalAddedItemsAmount = itemsAddedToSameItemSlot + itemsAddedToAvailableSlotAmount;

            return new AddToIventoryResult(amount, totalAddedItemsAmount);
        }

        public int GetAmount(string item)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var _slots = new IReadOnlyInventorySlot[Size.x, Size.y];
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    _slots[i, j] = _slotsMap[new Vector2Int(i, j)];
                }
            }

            return _slots;
        }

        public bool Has(string item, int amount)
        {
            throw new NotImplementedException();
        }

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            var _size = data._size;
            for (int i = 0; i < Size.x; i++) 
            { 
                for (int j = 0; j < Size.y; j++)
                {
                    var index = i * _size.y + j;
                    var slotData = _data._slots[index];
                    var _slot = new InventorySlot(slotData);
                    _slotsMap[new Vector2Int(i, j)] = _slot;
                }
            }
        }
    }
}
