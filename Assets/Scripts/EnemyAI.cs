using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        RangedIdle,
        Idle,
        Patrol,
        Chase,
        Attack,
        AttackRanged,
        Dead
    }

    private State currentState;

    [Header("Movement Settings")]
    [SerializeField] private Transform[] patrolPoints;
    private int currentPointIndex = 0;

    [Header("Health Settings")]
    private float currentHealth;

    private float lastAttackTime;

    [Header("References")]
    [SerializeField] private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;

    [Header("Animator Keys")]
    private const string IS_WALKING = "isWalking";
    private const string DIE = "dies";
    private const string ATTACK = "attack";
    private const string HURT = "hurt";

    [Header("Enemy Data")]
    public EnemyDataSO EnemyDataSO;
    

    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private GameObject _projectilePrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = EnemyDataSO.maxHealth;

        playerHealth = player.GetComponent<PlayerHealth>();
        playerLevel = player.GetComponent<PlayerLevel>();

        switch (EnemyDataSO.enemyType)
        {
            case EnemyType.Melee:
                currentState = State.Patrol;
                break;
            case EnemyType.Ranged:
                currentState = State.RangedIdle;
                break;
        }
    }

    void Update()
    {
        if (currentState == State.Dead) return;

        HandleState();
    }

    // =========================
    // STATE MACHINE
    // =========================
    private void HandleState()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        
        switch (currentState)
        {
            case State.Idle:
                if (distance <= EnemyDataSO.attackRange)
                    ChangeState(State.Attack);
                break;

            case State.RangedIdle:
                if (distance <= EnemyDataSO.detectionRange)
                    ChangeState(State.AttackRanged);
                break;

            case State.Patrol:
                Patrol();

                if (distance < EnemyDataSO.detectionRange)
                    ChangeState(State.Chase);
                break;

            case State.Chase:
                Chase();

                if (distance <= EnemyDataSO.attackRange)
                    ChangeState(State.Attack);

                else if (distance > EnemyDataSO.detectionRange)
                    ChangeState(State.Patrol);

                break;

            case State.Attack:
                Attack(EnemyDataSO.damage);

                if (distance > EnemyDataSO.attackRange)
                    ChangeState(State.Chase);

                break;

            case State.AttackRanged:
                ShootProjectile();

                if (distance > EnemyDataSO.attackRange)
                    ChangeState(State.RangedIdle);

                break;
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
    }

    // =========================
    // BEHAVIORS
    // =========================

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPointIndex];
        MoveTowards(target.position);

        anim.SetBool(IS_WALKING, true);

        if (Mathf.Abs(transform.position.x - target.position.x) < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void Chase()
    {
        MoveTowards(player.position);
        anim.SetBool(IS_WALKING, true);
    }

    private void Attack(float damageInflicted)
    {
        rb.velocity = Vector2.zero;
        anim.SetBool(IS_WALKING, false);

        if (Time.time >= lastAttackTime + EnemyDataSO.attackCooldown)
        {
            anim.SetTrigger(ATTACK);

            playerHealth.ApplyDamage(damageInflicted);

            lastAttackTime = Time.time;
        }
    }

    private void ShootProjectile()
    {
        if (_projectilePrefab == null || _firePoint == null) return;

        if (Time.time >= lastAttackTime + EnemyDataSO.attackCooldown)
        {
            anim.SetTrigger(ATTACK);

            GameObject proj = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);

            Vector2 direction = (player.position - _firePoint.position).normalized;

            proj.GetComponent<Rigidbody2D>().velocity = direction * 6f;

            lastAttackTime = Time.time;
        }

        
    }

    private void MoveTowards(Vector2 target)
    {
        float direction = target.x > transform.position.x ? 1 : -1;

        rb.velocity = new Vector2(direction * EnemyDataSO.moveSpeed, rb.velocity.y);

        transform.localScale = new Vector3(direction, 1, 1);
    }

    // =========================
    // DAMAGE & DEATH
    // =========================

    public void TakeDamage(float damage)
    {
        if (currentState == State.Dead) return;

        currentHealth -= damage;
        anim.SetTrigger(HURT);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        currentState = State.Dead;

        GiveXP();

        anim.SetTrigger(DIE);

        StartCoroutine(DeathRoutine());
    }

    private void GiveXP()
    {
        int xp = EnemyDataSO.enemyType switch
        {
            EnemyType.Melee => 10,
            EnemyType.Tank => 50,
            _ => 20
        };

        playerLevel.GainXP(xp);
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
