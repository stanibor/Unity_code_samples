using UnityEngine;
using System.Collections;

public class MouseController : IController
{
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;
    bool moved = true;


    public override bool Moved()
    {
        return moved;
    }

    public override Vector3 MoveVector()
    {
        return Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
    }

    public override Vector3 TurnVector()
    {
        floorMask = LayerMask.GetMask("Floor");
        //moved = false;
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        Vector3 playerToMouse = Vector3.forward;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            moved = true;
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - affected.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;
        }

        return playerToMouse;
    }

    public override float SpawnAxis()
    {
        return Input.GetAxisRaw("Spawn");
    }

    public override float ShootAxis()
    {
        return Input.GetAxisRaw("Shoot");
    }
}
