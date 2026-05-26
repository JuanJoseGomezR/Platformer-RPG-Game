using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform gridParent;
    public GameObject slotPrefab;

    private InventorySlotUI[] slots;

    private void Start()
    {
        slots = new InventorySlotUI[InventoryManager.Instance.inventorySize];

        for (int i = 0; i < slots.Length; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, gridParent);
            slots[i] = newSlot.GetComponent<InventorySlotUI>();
        }

        InventoryManager.Instance.OnInventoryChanged += UpdateUI;
        UpdateUI();

        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        var inventory = InventoryManager.Instance.inventory;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Count)
            {
                slots[i].SetSlot(inventory[i]);
            }
            else
            {
                slots[i].SetSlot(null);
            }
        }
    }
}
