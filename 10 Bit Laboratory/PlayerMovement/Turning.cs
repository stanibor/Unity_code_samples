using UnityEngine;
using System.Collections;

public class Turning : MonoBehaviour
{
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;

    public float Maxangle = 65f;

    ControllerMenager CMenager;

    // Use this for initialization
    void Start ()
    {
        floorMask = LayerMask.GetMask("Floor");
        CMenager = GetComponentInParent<ControllerMenager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Turn();
	}

    void Turn()
    {

        // Perform the raycast and if it hits something on the floor layer...
        if (CMenager.Control.Moved())
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = CMenager.Control.TurnVector();

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse,Vector3.up);
            float temp = newRotation.eulerAngles.y - 90f;

            //Debug.Log(temp);
            // Set the player's rotation to this new rotation.
            if ( (temp <= Maxangle && temp >=-Maxangle) || (temp - 180f <= Maxangle && temp - 180f >= -Maxangle))
                transform.rotation = newRotation;
        }
    }
}
