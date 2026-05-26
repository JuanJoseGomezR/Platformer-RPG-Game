using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    Consumable,
    Weapon,
    Armor,
    Material
}

public enum EquipmentType
{ 
    None,
    Helmet,
    Armor,
    Boots,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemDataSO : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;
    public ItemType type;
    [TextArea] public string description;

    [Header("Stacking")]
    public bool isStackable;
    public int maxStack;

    [Header("Stats (Optional)")]
    public EquipmentType equipmentType;
    public CharacterStats bonusStats;

    [Header("Consumable")]
    public bool isConsumable;
    public bool restoresHealth;
    public float healthRestored;
    public float duration; // seconds
}
