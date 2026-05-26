using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemDataSO ItemData;
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            bool isAdded = InventoryManager.Instance.AddItemToInventory(ItemData, amount);

            if (isAdded)
            {
                Destroy(gameObject);
            }
        }
    }
}
