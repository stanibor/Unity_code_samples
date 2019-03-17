using UnityEngine;
using System.Collections;

public class Tracer : MonoBehaviour
{
    public Transform Target;
    public float radius = 0.2f;

    Vector3 Origin;
    Vector3 Hue;

    Quaternion rotor;



	// Use this for initialization
	void Start ()
    {
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Origin = transform.position;
        //Debug.Log(Origin);
        Hue = Vector3.zero;
        rotor = transform.rotation * Quaternion.Euler(-90, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Origin = transform.position - Hue;
        //Debug.Log(Origin);
        Hue = Target.position - Origin;
        Hue.Normalize();
        Hue *= radius;
        
        Hue = rotor * Hue;
        transform.position = (Origin + Hue);
        

    }
}
