using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance;

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    public GameObject numbersHolder;


    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject toolHolder;
    public GameObject itemModel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach (Transform number in numbersHolder.transform)
        {
            number.Find("numText").GetComponent<Text>().color = Color.grey;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1)) 
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
    }

    void SelectQuickSlot(int number)
    {
        if (selectedNumber != number)
        {
            selectedNumber = number;

            if (selectedItem != null)
            {
                selectedItem.GetComponent<InventoryItem>().isSelected = false;
            }

            if (slotIsFull(number))
            {
                selectedItem = GetSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquipedModel(selectedItem);

                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("numText").GetComponent<Text>().color = Color.grey;
                }

                Text toBeChanged = numbersHolder.transform.Find("number" + number).transform.Find("numText").GetComponent<Text>();
                toBeChanged.color = Color.white;
            }
        }
        else
        {
            selectedNumber = -1;

            if (selectedItem != null)
            {
                selectedItem.GetComponent<InventoryItem>().isSelected = false;
                selectedItem = null;
            }

            if (itemModel != null)
            {
                Destroy(itemModel.gameObject);
                itemModel = null;
            }

            foreach (Transform child in numbersHolder.transform)
            {
                child.transform.Find("numText").GetComponent<Text>().color = Color.grey;
            }
        }
        
    }

    void SetEquipedModel(GameObject selctedItem)
    {
        if (itemModel != null)
        {
            Destroy(itemModel.gameObject);
            itemModel = null;
        }

        string name = selectedItem.name.Replace("Icon(Clone)", "");
        itemModel = Instantiate(Resources.Load<GameObject>(name), 
            new Vector3(0.6f, 0f, 1.2f), Quaternion.Euler(0, -95, -10));
        itemModel.transform.SetParent(toolHolder.transform, false);
    }

    GameObject GetSelectedItem(int number)
    {
        return quickSlotsList[number - 1].transform.GetChild(0).gameObject;
    }

    bool slotIsFull(int number)
    {
        if (quickSlotsList[number - 1].transform.childCount > 0)
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting clean name
        string cleanName = itemToEquip.name.Replace("Icon(Clone)", "");
        // Adding item to list
        itemList.Add(cleanName);

        InventorySystem.instance.RecalculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
