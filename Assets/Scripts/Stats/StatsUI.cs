using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [Header("Player Stats Reference")]
    [SerializeField]
    private CharacterCombat _characterCombat;

    [Header("Stats Sliders")]
    [SerializeField]
    private Slider _vitalitySlider;
    [SerializeField]
    private Slider _strengthSlider;
    [SerializeField]
    private Slider _agilitySlider;
    [SerializeField]
    private Slider _critChanceSlider;

    [Header("Stats Texts")]
    [SerializeField]
    private TextMeshProUGUI _vitalityText;
    [SerializeField]
    private TextMeshProUGUI _strengthText;
    [SerializeField]
    private TextMeshProUGUI _agilityText;
    [SerializeField]
    private TextMeshProUGUI _critChanceText;

    private void Start()
    {
        _characterCombat.OnStatsChanged += UpdateStatsUI;
        UpdateStatsUI();
    }


    private void UpdateStatsUI() 
    {
        CharacterStats currentStats = _characterCombat.baseStats;

        _vitalitySlider.value = currentStats.vitality;
        _strengthSlider.value = currentStats.strength;
        _agilitySlider.value = currentStats.agility;
        _critChanceSlider.value = currentStats.critChance;


        _vitalityText.text = $"Vitality: {currentStats.vitality}";
        _strengthText.text = $"Strength: {currentStats.strength}";
        _agilityText.text = $"Agility: {currentStats.agility}";
        _critChanceText.text = $"Crit. Chance: {currentStats.critChance}%";
    }

    private void OnDestroy()
    {
        _characterCombat.OnStatsChanged -= UpdateStatsUI;
    }
}
