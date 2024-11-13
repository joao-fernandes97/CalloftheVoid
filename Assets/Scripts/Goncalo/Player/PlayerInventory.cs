using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    private List<GameObject> inventorySlots = new List<GameObject>();
    private int currentItem = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            //Gets all the available inventorty slots
            inventorySlots.Add(inventory.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Hides/shows the inventory and the items
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
        }

        //Uses the item if possible
        if (Input.GetMouseButtonDown(0) && inventorySlots[currentItem].activeInHierarchy)
        {
            inventorySlots[currentItem].transform.GetChild(0).GetComponent<IUsable>().Use();
        }

        ChangeItem();
    }

    /// <summary>
    /// Deactivates all the inventory slots and activates the new item
    /// </summary>
    /// <param name="item"></param>
    public void NewItem(int item)
    {
        foreach (GameObject slot in inventorySlots)
        {
            slot.SetActive(false);
        }

        Debug.Log(inventorySlots[item]);
        inventorySlots[item].SetActive(true);
        currentItem = item;
    }

    /// <summary>
    /// Detects player input to change which item is active
    /// </summary>
    private void ChangeItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventorySlots[0].transform.childCount > 0)
        {
            NewItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventorySlots[1].transform.childCount > 0)
        {
            NewItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventorySlots[2].transform.childCount > 0)
        {
            NewItem(2);
        }
    }
}
