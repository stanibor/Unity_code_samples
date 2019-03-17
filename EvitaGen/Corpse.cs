using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    [SerializeField]
    float explosionForce = 10f;

	// Use this for initialization
	void Start ()
    {
        Rigidbody2D[] derbies = GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D derbie in derbies)
        {
            Vector2 force = derbie.transform.position - transform.position;
            force.Normalize();
            derbie.AddForce(force * explosionForce);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
