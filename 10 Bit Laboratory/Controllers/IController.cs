using UnityEngine;
using System.Collections;

public abstract class IController
{
    protected Transform affected;
    public Transform Affected
    {
        set { affected = value; }
    }

    public abstract bool Moved();
    public abstract Vector3 MoveVector();
    public abstract Vector3 TurnVector();
    public abstract float SpawnAxis();
    public abstract float ShootAxis();
}
