using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int inventorySize = 2;

    public List<InventorySlot> inventory = new List<InventorySlot>();

    public Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool AddItemToInventory(ItemDataSO item, int amount = 1)
    {
        if (item == null)
        {
            return false;
        }

        InventorySlot newSlot = new InventorySlot(item, amount);

        inventory.Add(newSlot);

        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItemFromInventory(ItemDataSO itemToRemove, int amount = 1) 
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == itemToRemove)
            {
                inventory[i].RemoveAmount(amount);

                if (inventory[i].amount <= 0)
                {
                    inventory.RemoveAt(i);
                }

                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }

    public InventorySaveData GetSaveData()
    {
        return new InventorySaveData
        {
            inventory = this.inventory,
        };
    }

    public void LoadFromData(InventorySaveData data)
    {
        inventory = data.inventory;

        OnInventoryChanged?.Invoke();
    }
}
