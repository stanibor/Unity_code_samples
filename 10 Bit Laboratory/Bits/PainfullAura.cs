using UnityEngine;
using System.Collections;

public class PainfullAura : MonoBehaviour
{
    public float Multiplier = 0.1f;

    public float PushBackForce = 10f;

    PlayerHealth Loser;

    Rigidbody Rigid;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null && other.tag != "Etheric")
        {
            Vector3 Push = other.transform.position - transform.position;
            Push.Normalize();
            Push *= PushBackForce;

            other.GetComponent<Rigidbody>().AddForce(Push);
        }

        if(other.tag == "Player")
        {
            Loser = other.GetComponent<PlayerHealth>();
            Loser.TakeDamage(Rigid.velocity.magnitude * Multiplier);
        }
    }

	// Use this for initialization
	void Start ()
    {
        Rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
