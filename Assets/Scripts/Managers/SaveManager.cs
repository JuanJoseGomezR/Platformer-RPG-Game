using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string saveFolder;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFolder = Application.persistentDataPath + "/Saves/";

        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);
    }

    public void SaveGame(int slotIndex)
    {
        string path = saveFolder + "slot_" + slotIndex + ".json";

        GameSaveData data = BuildSaveData();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Game saved to slot " + slotIndex);
    }

    public void LoadGame(int slotIndex)
    {
        string path = saveFolder + "slot_" + slotIndex + ".json";

        if (!File.Exists(path))
        {
            Debug.Log("No save file found in slot " + slotIndex);
            return;
        }

        string json = File.ReadAllText(path);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

        RestoreSaveData(data);

        Debug.Log("Loaded slot " + slotIndex);
    }

    public void AutoSave()
    {
        string path = saveFolder + "autosave.json";

        GameSaveData data = BuildSaveData();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public bool SlotExists(int slotIndex)
    {
        string path = saveFolder + "slot_" + slotIndex + ".json";
        return File.Exists(path);
    }

    public GameSaveData GetSlotPreview(int slotIndex)
    {
        string path = saveFolder + "slot_" + slotIndex + ".json";

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    private GameSaveData BuildSaveData()
    {
        GameSaveData data = new GameSaveData();

        data.saveDate = System.DateTime.Now.ToString();
        data.playTime = Time.time;

        data.playerProgress = FindObjectOfType<PlayerLevel>().GetSaveData();
        data.inventoryData = InventoryManager.Instance.GetSaveData();
        //data.equipmentData = FindObjectOfType<Equipment>().GetSaveData();

        return data;
    }

    private void RestoreSaveData(GameSaveData data)
    {
        if (data == null) return;

        // Player Level
        PlayerLevel playerLevel = FindObjectOfType<PlayerLevel>();
        if (playerLevel != null && data.playerProgress != null)
        {
            playerLevel.LoadFromData(data.playerProgress);
        }

        // Inventory
        InventoryManager inventory = InventoryManager.Instance;
        if (inventory != null && data.inventoryData != null)
        {
            inventory.LoadFromData(data.inventoryData);
        }

        // Equipment
        /*** Equipment equipment = FindObjectOfType<Equipment>();
        if (equipment != null && data.equipmentData != null)
        {
            equipment.LoadFromData(data.equipmentData);
        } ***/
    }
}
