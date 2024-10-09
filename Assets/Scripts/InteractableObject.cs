using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)&& playerInRange) 
        {
            //if the inventory is not full
            if (!InventorySystem.Instance.CheckIffull())
            {
                InventorySystem.Instance.AddToInvetory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}