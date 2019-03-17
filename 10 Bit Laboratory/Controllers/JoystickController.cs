using UnityEngine;
using System.Collections;

public class JoystickController : IController
{
    bool moved = true;


    public override bool Moved()
    {
        return Input.GetAxisRaw("JVertical") != 0f || Input.GetAxisRaw("JHorizontal") != 0f;
    }

    public override Vector3 MoveVector()
    {
        return Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
    }

    public override Vector3 TurnVector()
    {
        return Vector3.forward * Input.GetAxisRaw("JVertical") + Vector3.right * Input.GetAxisRaw("JHorizontal"); ;
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
