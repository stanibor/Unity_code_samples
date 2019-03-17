using UnityEngine;
using System.Collections;

public class RotPreserver : MonoBehaviour
{
    public Vector3 Rotation;

	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Euler(Rotation);
	}
}
