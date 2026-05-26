using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public int saveVersion = 1;

    public string saveDate;
    public float playTime;

    public PlayerProgessData playerProgress;
    public InventorySaveData inventoryData;
    public EquipmentSaveData equipmentData;
}
