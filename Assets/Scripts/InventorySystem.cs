using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
    public bool isOpen;
    public bool isFull;



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
    }


    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList(); 

    }

    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform) {

            if (child.CompareTag("Slot")){
                slotList.Add(child.gameObject);
            }
        
        
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    void OnGUI()
    {
        if (isOpen)
        {
            // This ensures that the cursor remains unlocked and visible while inventory is open
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void AddToInvetory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();

        if (whatSlotToEquip == null)
        {
            Debug.Log("No empty slot found");
            return;
        }

        GameObject itemToAdd = Resources.Load<GameObject>(itemName);
        if (itemToAdd == null)
        {
            Debug.Log("Item " + itemName + " not found in Resources");
            return;
        }

        itemToAdd = Instantiate(itemToAdd, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemList.Add(itemName);
    }

    private GameObject FindNextEmptySlot()
    {
        
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject() ;
    }

    public bool CheckIffull()
    {
        int counter = 0;
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if(counter == 24)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}