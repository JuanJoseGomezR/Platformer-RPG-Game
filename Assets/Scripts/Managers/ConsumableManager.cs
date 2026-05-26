using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
    public static ConsumableManager Instance;

    [SerializeField]
    private CharacterCombat combat;

    void Awake()
    {
        Instance = this;
    }

    public void UseConsumable(ItemDataSO item)
    {
        StartCoroutine(ApplyBuff(item));
    }

    private IEnumerator ApplyBuff(ItemDataSO item)
    {
        if (item.restoresHealth)
        {
            combat.RestoreHealth(item.healthRestored);

            yield return new WaitForEndOfFrame();
        }
        else
        {
            Debug.Log("Using consumable: " + item.itemName);

            combat.AddBonusStats(item.bonusStats);

            yield return new WaitForSeconds(item.duration);

            combat.RemoveBonusStats(item.bonusStats);

            Debug.Log("Buff expired: " + item.itemName);
        }
    }
}
