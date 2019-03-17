using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public GameObject Cannon;                                
    public Slider healthSlider;
    public GunHandler Shooter;                                 

    Animator anim;                                              
    PlayerMovement playerMovement;                              
    bool isDead;                                               
    bool damaged;

    public bool isDead_t { get { return isDead; } set { isDead = value; } }

    GameObject temp;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            anim.SetTrigger("Pain");
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(float amount)
    {
        // Set the damaged flag so the screen will flash.
        if (amount >= 0.01f * startingHealth)
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        if (currentHealth >= startingHealth)
            currentHealth = startingHealth;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth/startingHealth;


        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }

    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.

        // Tell the animator that the player is dead.
        anim.SetBool("Dead",true);
        anim.SetTrigger("Die");

        Shooter.enabled = false;

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        //playerShooting.enabled = false;
        Invoke("GenerateCannon", 0.2f);
    }

    void GenerateCannon()
    {
        temp = (GameObject)Instantiate(Cannon, transform.position + (Vector3.up * 2.5f), Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce(Vector3.up * 800f);
    } 
}
