using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI amountText;

    [SerializeField]
    private ItemTooltip _tooltip;

    private InventorySlot _slot;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_slot != null && _slot.item != null)
        {
            UIManager.Instance.ShowTooltip(_slot.item);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_slot == null || _slot.item == null) return;

        // Right click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideTooltip();
    }

    public void SetSlot(InventorySlot newSlot)
    {
        _slot = newSlot;

        if (_slot == null || _slot.item == null)
        {
            icon.enabled = false;
            amountText.text = "";
            return;
        }

        icon.enabled = true;
        icon.sprite = _slot.item.icon;

        amountText.text = _slot.amount > 1 ? _slot.amount.ToString() : "";
    }

    public void UseItem()
    {
        if (_slot.item.type == ItemType.Armor)
        {
            EquipmentManager.Instance.Equip(_slot.item);
        }
        else if (_slot.item.type == ItemType.Consumable)
        {
            ConsumableManager.Instance.UseConsumable(_slot.item);

            // Remove item from inventory after use
            _slot.amount--;

            if (_slot.amount <= 0)
            {
                _slot.item = null;
            }

            // Refresh UI
            InventoryManager.Instance.OnInventoryChanged?.Invoke();
        }
    }
}
