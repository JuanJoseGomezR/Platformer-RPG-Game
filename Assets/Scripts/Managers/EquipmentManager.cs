using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    [SerializeField]
    private CharacterCombat combat;

    private Dictionary<EquipmentType, ItemDataSO> equippedItems =
        new Dictionary<EquipmentType, ItemDataSO>();

    private void Awake()
    {
        Instance = this;

        combat = GetComponent<CharacterCombat>();
    }

    public void Equip(ItemDataSO item)
    {
        if (item.type != ItemType.Armor) return;

        EquipmentType type = item.equipmentType;

        // Unequip if slot already used
        if (equippedItems.ContainsKey(type))
        {
            Unequip(type);
        }

        equippedItems[type] = item;

        combat.AddBonusStats(item.bonusStats);

        Debug.Log("Equipped: " + item.itemName);
    }

    public void Unequip(EquipmentType type)
    {
        if (!equippedItems.ContainsKey(type)) return;

        ItemDataSO item = equippedItems[type];

        combat.RemoveBonusStats(item.bonusStats);

        equippedItems.Remove(type);

        Debug.Log("Unequipped: " + item.itemName);
    }
}
