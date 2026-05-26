using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.ApplyDamage(30f); // you can pass damage later if needed
            }

            Destroy(gameObject);
        }
        /*else if (collision.CompareTag("Ground Layer"))
        {
            Destroy(gameObject);
        }*/
    }
}
