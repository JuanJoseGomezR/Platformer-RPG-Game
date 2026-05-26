using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{ 
    Melee,
    Ranged,
    Tank,
    Fast
}

[CreateAssetMenu(menuName = "Enemy/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public string EnemyName = "";

    [Header("Stats")]
    public float maxHealth;
    public float damage;
    public float moveSpeed;

    [Header("Behavior")]
    public EnemyType enemyType;
    public float detectionRange;
    public float attackRange;
    public float attackCooldown;
}
