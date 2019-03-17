using UnityEngine;
using System.Collections;

public class CollideAnihilate : MonoBehaviour
{
    public string ReactionTag = "Player";
    public GameObject AfterEffect;
    public bool EasilyDestructable;

	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
	    if((other.tag == ReactionTag || EasilyDestructable)&&(other.tag != "Etheric"))
        {
            Instantiate(AfterEffect, transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(gameObject);
        }

	}
	

}
