using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject pauseMenu;

    [Header("Movement Default Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 8f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float attackRange = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    [SerializeField]
    private LayerMask _deathPoint;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public LayerMask enemyLayers;
    private PlayerHealth _playerHealth;
    private CharacterCombat _characterCombat;

    [SerializeField]
    private Transform attackPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _characterCombat = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        if (_playerHealth.IsDead) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            EventSystem.current.SetSelectedGameObject(inventoryPanel);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        // 1. Horizontal Movement
        moveInput = Input.GetAxisRaw("Horizontal");
        float finalMoveSpeed = moveSpeed + _characterCombat.FinalStats.agility * 0.2f;

        rb.velocity = new Vector2(moveInput * finalMoveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true);

            if (moveInput == 1)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput == -1)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        // 2. Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // 3. Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerAttack();
        }

        // 4. "Mario" Jump Physics (Better Weight)
        ApplyBetterGravity();
    }

    void ApplyBetterGravity()
    {
        // If we are falling, increase gravity for a snappy landing
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // If we let go of the jump button early, start falling sooner
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void PickUpItem()
    { 
    
    }

    private void PlayerAttack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            _characterCombat.Attack(enemy.GetComponent<EnemyAI>());
        }
    
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
