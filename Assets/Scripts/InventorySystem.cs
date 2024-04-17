using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<GameObject> InventorySlots = new List<GameObject>();
    public List<string> InventoryItems = new List<string>();

    private GameObject itemToAdd;
    private GameObject slotToEquip;

    public GameObject inventoryScreenUI;

    public bool isFull;
    public bool isOpen = false;

    #region MONO

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        inventoryScreenUI.SetActive(false);
    }

    
    private void Start()
    {
        isFull = false;
        isOpen = false;
        PopulateInventory();

        EventSystem.Collect += AddInInventory;
    }

    #endregion

    private void PopulateInventory()
    {
        foreach (Transform slot in inventoryScreenUI.transform)
        {
            if (slot.CompareTag("Slot"))
            {
                InventorySlots.Add(slot.gameObject);
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            if (CraftingSystem.Instance.isOpen)
            {
                CraftingSystem.Instance.craftingScreenUI.SetActive(false);
                CraftingSystem.Instance.isOpen = false;
            }
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    public void AddInInventory(int countOfResources, string ItemName)
    {
        for (int i = 0; i < countOfResources; i++)
        {
            slotToEquip = FindEmptySlot();

            itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName + "Icon"), slotToEquip.transform.position, slotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(slotToEquip.transform);

            InventoryItems.Add(ItemName);

            if (itemToAdd.GetComponent<InventoryItem>().isEquippable)
            {
                EquipSystem.Instance.AddToQuickSlots(itemToAdd);
            }

            RecalculateList();

            CraftingSystem.Instance.RefreshNeededItems();
        }
        
    }

    public bool CheckNFull()
    {
        int counter = 0;

        foreach(GameObject slot in InventorySlots)
        {
            if(slot.transform.childCount > 0)
            {
                counter++;
            }
        }
        if (counter == 18)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject FindEmptySlot()
    {
        foreach (GameObject slot in InventorySlots)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public void RemoveItems(string nameToRemove, int count)
    {
        int counter = count;

        for (int i = InventorySlots.Count - 1; i >= 0; i--)
        {
            Transform currentSlot = InventorySlots[i].transform;
            if (currentSlot.childCount > 0)
            {
                if (currentSlot.GetChild(0).name.Equals(nameToRemove + "Icon(Clone)") && counter != 0)
                {
                    DestroyImmediate(currentSlot.GetChild(0).gameObject);

                    counter--;
                }
            }
        }

        RecalculateList();

        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void RecalculateList()
    {
        InventoryItems.Clear();

        foreach (GameObject slot in InventorySlots)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;

                InventoryItems.Add(name.Replace("Icon(Clone)", ""));
            }
        }
    }
}
