using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    public CharacterStats baseStats;

    private CharacterStats bonusStats = new CharacterStats();

    public event Action OnStatsChanged;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public CharacterStats FinalStats
    {
        get
        {
            CharacterStats final = new CharacterStats();

            final.strength = baseStats.strength + bonusStats.strength;
            final.agility = baseStats.agility + bonusStats.agility;
            final.vitality = baseStats.vitality + bonusStats.vitality;

            final.maxHealth = (final.vitality * 10);
            final.damage = final.strength * 2;
            final.critChance = final.agility * 0.5f;

            return final;
        }
    }

    public void RestoreHealth(float amount)
    {
        _playerHealth.RestoreHealth(amount);
    }

    public void AddBonusStats(CharacterStats stats)
    {
        bonusStats.strength += stats.strength;
        bonusStats.agility += stats.agility;
        bonusStats.vitality += stats.vitality;

        OnStatsChanged?.Invoke();
    }

    public void RemoveBonusStats(CharacterStats stats)
    {
        bonusStats.strength -= stats.strength;
        bonusStats.agility -= stats.agility;
        bonusStats.vitality -= stats.vitality;

        OnStatsChanged?.Invoke();
    }

    public void Attack(EnemyAI enemy)
    {
        if (enemy == null) return;

        int damage = FinalStats.damage;

        float critRoll = UnityEngine.Random.Range(0f, 100f);
        if (critRoll < FinalStats.critChance)
        {
            damage *= 2;
            Debug.Log("Critical Hit!");
        }

        enemy.TakeDamage(damage);
    }

    public void IncreaseStats()
    {
        baseStats.strength += 2;
        baseStats.vitality += 2;
        baseStats.agility += 1;
    }
}
