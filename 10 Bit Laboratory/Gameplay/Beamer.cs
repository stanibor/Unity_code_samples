using UnityEngine;
using System.Collections;

public class Beamer : MonoBehaviour
{
    public float AcumulatedPower = 500f;
    public float MaxPower = 5000f;

    GameObject Beamed;
    Rigidbody Temp;
    ParticleSystem Blob;

    float quant = 50f;

    void Start()
    {
        Blob = GetComponent<ParticleSystem>();
        Destroy(gameObject, 12f);
        quant = 0.1f * AcumulatedPower;
    }

	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Floor" && other.tag != "Wall" && other.tag != "Etheric")
        {
            Beamed = other.gameObject;
            Temp = Beamed.GetComponent<Rigidbody>();
        }
        if (other.tag == "Wall")
            Destroy(gameObject);

        if(other.tag == "Bit")
        {
            EnemyHealth Sucker = other.GetComponent<EnemyHealth>();
            Sucker.TakeDamage(quant = 0.1f * AcumulatedPower);
        }
       
	}

    void OnTriggerExit(Collider other)
    {
        Beamed = null;
    }

    // Update is called once per frame
    void Update ()
    {
        Blob.startSize = 1f + (3f * AcumulatedPower) / MaxPower;

        Debug.Log("AC: " + AcumulatedPower);
        if (Beamed != null && AcumulatedPower > 0f)
        {
            Temp.AddForce(transform.rotation * Vector3.forward * quant);
            AcumulatedPower -= quant;
        }

        if (AcumulatedPower <= 0f)
            Destroy(gameObject);
	}
}
