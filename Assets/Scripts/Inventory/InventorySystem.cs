using Inventory;
using Inventory.Controllers;
using Inventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;

        
        private InventoryGrid _inventoryGrid;
        private InventoryController _inventory;

        #region MONO

        private void Awake()
        {
        }


        private void Start()
        {
            var slots = new List<SlotData>();
            for (var i = 0; i < 5; i++)
            {
                slots.Add(new SlotData());
            }

            InventoryGridData gridData = new InventoryGridData
            {
                _size = new Vector2Int(1, 5),
                _slots = slots
            };

            _inventoryGrid = new InventoryGrid(gridData);

            _inventory = new InventoryController(_inventoryGrid, _inventoryView);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                _inventoryGrid.AddItems("Meat");
                foreach (var i in _inventoryGrid.GetSlots())
                {
                    Debug.Log("Item " + i.Item);
                }
            }
        }

        #endregion


    }
}
