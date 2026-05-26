using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    public float xpGrowthMultiplier = 1.5f;

    public event Action OnXPChanged;
    public event Action OnLevelUp;

    private CharacterCombat _characterCombat;

    void Awake()
    {
        _characterCombat = GetComponent<CharacterCombat>();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        OnXPChanged?.Invoke();

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpGrowthMultiplier);

        _characterCombat.IncreaseStats();

        OnLevelUp?.Invoke();
        OnXPChanged?.Invoke();
    }

    public PlayerProgessData GetSaveData()
    {
        return new PlayerProgessData
        {
            level = currentLevel,
            currentXP = currentXP,
            xpToNextLevel = xpToNextLevel
        };
    }

    public void LoadFromData(PlayerProgessData data)
    {
        currentLevel = data.level;
        currentXP = data.currentXP;
        xpToNextLevel = data.xpToNextLevel;

        OnXPChanged?.Invoke();
        OnLevelUp?.Invoke();
    }
}
