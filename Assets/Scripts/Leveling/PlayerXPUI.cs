using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXPUI : MonoBehaviour
{
    public PlayerLevel playerLevel;
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    void Start()
    {
        playerLevel.OnXPChanged += UpdateXPUI;
        playerLevel.OnLevelUp += UpdateLevelUI;

        UpdateXPUI();
        UpdateLevelUI();
    }

    void UpdateXPUI()
    {
        xpSlider.maxValue = playerLevel.xpToNextLevel;
        xpSlider.value = playerLevel.currentXP;
    }

    void UpdateLevelUI()
    {
        levelText.text = playerLevel.currentLevel.ToString();
    }

    void OnDestroy()
    {
        playerLevel.OnXPChanged -= UpdateXPUI;
        playerLevel.OnLevelUp -= UpdateLevelUI;
    }
}
