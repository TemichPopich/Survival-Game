using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    public readonly struct AddToIventoryResult
    {
        public readonly int _itemsAddedAmount;
        public readonly int _itemsToAddAmount;

        public int ItemsNotAddedAmount => _itemsToAddAmount - _itemsAddedAmount;

        public AddToIventoryResult(int itemsToAddAmount, int itemsAddedAmount)
        {
            _itemsToAddAmount = itemsToAddAmount;
            _itemsAddedAmount = itemsAddedAmount;
        }
    }
}
