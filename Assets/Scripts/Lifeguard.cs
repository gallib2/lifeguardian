using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifeguard : MonoBehaviour
{
    public bool toAllowInventory;
    public static event Action OnItemAddToInventory;
    public InventoryObject inventory;

    private void OnEnable()
    {
        Item.OnItemClicked += AddItemToInventory;
    }

    private void OnDisable()
    {
        Item.OnItemClicked -= AddItemToInventory;
    }

    private void AddItemToInventory(ItemObject item)
    {
        if(toAllowInventory)
        {
            inventory.AddItem(item, 1);
            OnItemAddToInventory?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
         inventory.container.Clear();
    }
}
