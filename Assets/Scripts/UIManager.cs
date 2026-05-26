using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public ItemTooltip itemTooltip;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowTooltip(ItemDataSO item)
    {
        itemTooltip.ShowTooltip(item);
    }

    public void HideTooltip()
    {
        itemTooltip.HideTooltip();
    }
}
