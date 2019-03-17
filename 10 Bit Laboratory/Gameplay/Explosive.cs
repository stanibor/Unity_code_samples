using UnityEngine;
using System.Collections;

public class Explosive : MonoBehaviour
{
    public float ExplosionPower = 2000f;
    public float Radius = 2f;
    public float Damage = 25f;
    PlayerHealth Loser;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null && other.tag != "Etheric")
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(ExplosionPower, transform.position, Radius);
        }

        if(other.tag =="Player")
        {
            Loser = other.GetComponent<PlayerHealth>();
            Loser.TakeDamage(Damage);
        }
    }
}
