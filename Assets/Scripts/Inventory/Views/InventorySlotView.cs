using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.Views 
{
    public class InventorySlotView : MonoBehaviour, IDropHandler
    {
        [SerializeField] private string _itemName;
        [SerializeField] private TMP_Text _itemAmount;

        public string ItemName 
        { 
            get => _itemName;
            set
            {
                _itemName = value;
                OnSlotItemChanged(value);
            }
        }

        public int ItemAmount
        {
            get => Convert.ToInt32(_itemAmount.text);
            set => _itemAmount.text = value == 0 ? "" : value.ToString();
        }

        public void OnSlotItemChanged(string itemName)
        {
            var newItem = Instantiate(Resources.Load<GameObject>(ItemName + "Icon"), transform.position, transform.rotation);
            newItem.transform.SetParent(transform);
        }

        public GameObject Item
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }

                return null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            //if there is not item already then set our item.
            if (!Item)
            { 
                DragNDrop.itemBeingDragged.transform.SetParent(transform);
                DragNDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);
            }
        }
    }
}
