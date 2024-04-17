using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public List<string> inventoryItemList = new List<string>();
    public bool isOpen;
    Blueprint[] tools = {
        new Blueprint("Axe", "Wood", 1, "Stone", 2),
        new Blueprint("Hammer", "Wood", 1, "Stone", 3),
        new Blueprint("Armor", "Stone", 3, "Leather", 4),
        new Blueprint("Helmet", "Stone", 3, "Leather", 1),
        new Blueprint("Boots", "Stone", 2, "Leather", 2)}; 
    List<GameObject> toolsList = new List<GameObject>();
    public static CraftingSystem Instance;


    // All Blueprints


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
        craftingScreenUI.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        CreateListOfCraft();
        int i = 0;
        foreach (GameObject tool in toolsList)
        {
            Blueprint blueprint = tools[i];
            tool.GetComponent<Button>().onClick.AddListener(delegate { CraftAnyItem(blueprint); });
            ++i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // RefreshNeededItems();
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            if (InventorySystem.instance.isOpen)
            {
                InventorySystem.instance.inventoryScreenUI.SetActive(false);
                InventorySystem.instance.isOpen = false;
            }
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    private void CreateListOfCraft()
    {
        Vector3 position = new Vector3(craftingScreenUI.transform.position.x - 350, craftingScreenUI.transform.position.y + 80, 0f);
        for (int i = 0; i < tools.Length; i++)
        {
            if (i == 5)
            {
                position.y -= 250;
                position.x = craftingScreenUI.transform.position.x - 350;
            }
            GameObject tool = Instantiate(Resources.Load<GameObject>("Tool"));
            tool.transform.position = position;
            tool.transform.SetParent(craftingScreenUI.transform);
            position.x += 175;
            toolsList.Add(tool);

            Image icon = Instantiate(Resources.Load(tools[i].name + "Icon").GetComponent<Image>());
            icon.transform.position = tool.transform.Find("Image").position;
            icon.transform.SetParent(tool.transform);
            Destroy(tool.transform.Find("Image").gameObject);

            tool.transform.Find("Name").GetComponent<Text>().text = tools[i].name;
        }
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        InventorySystem inventory = InventorySystem.instance;

        // Remove resources fron inventory

        inventory.RemoveItems(blueprintToCraft.firstRequirement, blueprintToCraft.firstReqAmount);
        inventory.RemoveItems(blueprintToCraft.secondRequirement, blueprintToCraft.secondReqAmount);

        // Refresh list

        StartCoroutine(calculate());

        // Add item into inventory

        inventory.AddInInventory(1, blueprintToCraft.name);
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.instance.RecalculateList();
        RefreshNeededItems();
    }

    public void RefreshNeededItems()
    {
        inventoryItemList = InventorySystem.instance.InventoryItems;

        int stone_count = 0;
        int wood_count = 0;
        int leather_count = 0;
        bool req1 = false;
        bool req2 = false;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count++;
                    break;
                case "Wood":
                    wood_count++;
                    break;
                case "Leather":
                    leather_count++;
                    break;
            }
        }
        for (int i = 0; i < tools.Length; i++)
        {
            switch (tools[i].firstRequirement)
            {
                case "Stone":
                    toolsList[i].transform.Find("req1").GetComponent<Text>().text = $"{tools[i].firstReqAmount} {tools[i].firstRequirement} [{stone_count}]";
                    if (tools[i].firstReqAmount <= stone_count)
                    {
                       req1 = true;
                    }
                    break;
                case "Wood":
                    toolsList[i].transform.Find("req1").GetComponent<Text>().text = $"{tools[i].firstReqAmount} {tools[i].firstRequirement} [{wood_count}]";
                    if (tools[i].firstReqAmount <= wood_count)
                    {
                        req1 = true;
                    }
                    break;
                case "Leather":
                    toolsList[i].transform.Find("req1").GetComponent<Text>().text = $"{tools[i].firstReqAmount} {tools[i].firstRequirement} [{leather_count}]";
                    if (tools[i].firstReqAmount <= leather_count)
                    {
                        req1 = true;
                    }
                    break;
            }

            switch (tools[i].secondRequirement)
            {
                case "Stone":
                    toolsList[i].transform.Find("req2").GetComponent<Text>().text = $"{tools[i].secondReqAmount} {tools[i].secondRequirement} [{stone_count}]";
                    if (tools[i].secondReqAmount <= stone_count)
                    {
                        req2 = true;
                    }
                    break;
                case "Wood":
                    toolsList[i].transform.Find("req2").GetComponent<Text>().text = $"{tools[i].secondReqAmount} {tools[i].secondRequirement} [{wood_count}]";
                    if (tools[i].secondReqAmount <= wood_count)
                    {
                        req2 = true;
                    }
                    break;
                case "Leather":
                    toolsList[i].transform.Find("req2").GetComponent<Text>().text = $"{tools[i].secondReqAmount} {tools[i].secondRequirement} [{leather_count}]";
                    if (tools[i].secondReqAmount <= leather_count)
                    {
                        req2 = true;
                    }
                    break;
            }
            if (req1 && req2)
            {
                toolsList[i].GetComponent<Button>().interactable = true;
                req1 = false;
                req2 = false;
            }
            else
            {
                toolsList[i].GetComponent<Button>().interactable = false;
            }
        }
    }
}
