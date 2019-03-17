using UnityEngine;
using System.Collections;

public class Merger : MonoBehaviour
{
    public GameObject Effect;
    public GameObject successor;
    public float level = 0;

    bool superior = false;
    bool Done = false;
    Rigidbody Rigid;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bit")
        if(other.GetComponent<Merger>().level == level)
        {
            Rigidbody temp = other.GetComponent<Rigidbody>();
            if (gameObject.GetInstanceID() > other.gameObject.GetInstanceID())
            {
                superior = true;
                other.GetComponent<Merger>().Dismiss();
                
            }

            if (superior && !Done)
            {
                Instantiate(Effect, (temp.position + transform.position) / 2, temp.rotation);
                Done = true;
                Destroy(gameObject);
                Debug.Log(gameObject.GetInstanceID());
                Destroy(other.gameObject);
                //Destroy(gameObject);
                GameObject Temp = (GameObject)Instantiate(successor, (temp.position+transform.position)/2, temp.rotation);
                Temp.GetComponent<Rigidbody>().velocity = temp.velocity;
            }

        }
    }

    // Use this for initialization
    void Start ()
    {
        Rigid = GetComponent<Rigidbody>();
	}
	
    void Dismiss()
    {
        superior = false;
    }

    

}
