using UnityEngine;
using System.Collections;

public class TimeDestroyer : MonoBehaviour
{
    public float Timeout = 0.2f;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, Timeout);
	}
}
