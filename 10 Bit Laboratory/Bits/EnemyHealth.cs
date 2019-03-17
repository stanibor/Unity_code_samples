using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;

    public GameObject Effect;
    public GameObject FinalEffect;
    public GameObject PreviousForm;

    bool isDead;
    bool damaged;


    void Awake()
    {
        // Setting up the references.

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            Instantiate(Effect, transform.position, Quaternion.Euler(Vector3.zero));
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(float amount)
    {
        // Set the damaged flag so the screen will flash.
            damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        if (currentHealth >= startingHealth)
            currentHealth = startingHealth;

        // Set the health bar's value to the current health.


        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }

    }

    void Death()
    {
        isDead = true;
        Instantiate(FinalEffect, transform.position, Quaternion.Euler(Vector3.zero));
        Instantiate(PreviousForm, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
