using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool IsDead = false;

    private Animator animator;

    public event Action<float> OnHealthChanged;

    private CharacterCombat _characterCombat;

    private void Start()
    {
        _characterCombat = GetComponent<CharacterCombat>();
        maxHealth = _characterCombat.baseStats.maxHealth;

        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);

        animator = GetComponent<Animator>();
    }

    public void ApplyDamage(float damageInflicted)
    {
        if (IsDead) return;

        currentHealth -= 30f;
        OnHealthChanged?.Invoke(currentHealth);

        animator.SetTrigger("hurt");

        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    public void RestoreHealth(float amountRestored)
    {
        if (currentHealth + amountRestored > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amountRestored;
        }

        OnHealthChanged?.Invoke(currentHealth);

        Debug.Log("Restored " + amountRestored + " Health. Current health is:" + currentHealth);
    }

    public IEnumerator Death()
    {
        IsDead = true;
        //Apply logic when player dies
        animator.SetTrigger("dies");

        yield return new WaitForSeconds(2);

        RetryLevel();
    }

    public void InstantDeath()
    {
        IsDead = true;
        RetryLevel();
    }

    private void RetryLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    
    }
}
