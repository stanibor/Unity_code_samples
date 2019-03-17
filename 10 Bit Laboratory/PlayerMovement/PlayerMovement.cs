using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    ControllerMenager CMenager;

    void Awake()
    {
        // Create a layer mask for the floor layer.


        // Set up references.
        anim = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        CMenager = GetComponent<ControllerMenager>();
    }


    void FixedUpdate()
    {
        // Store the input axes.
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move();

        // Turn the player to face the mouse cursor.
        //Turning();

        // Animate the player.
        Animating();
    }

    void Move()
    {
        // Set the movement vector based on the axis input.
        movement = CMenager.Control.MoveVector();

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }


    void Animating()
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = (movement.magnitude != 0);

        // Tell the animator whether or not the player is walking.
        anim.SetBool("Running", walking);
    }
}
