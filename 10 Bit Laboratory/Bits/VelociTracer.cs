using UnityEngine;
using System.Collections;

public class VelociTracer : MonoBehaviour
{
    public float radius = 0.2f;

    Vector3 Origin;
    Vector3 Hue;

    Quaternion rotor;

    Rigidbody Rigid;



    // Use this for initialization
    void Start()
    {
        Rigid = GetComponentInParent<Rigidbody>();
        Origin = transform.position;
        //Debug.Log(Origin);
        Hue = Vector3.zero;
        rotor = transform.rotation * Quaternion.Euler(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Origin = transform.position - Hue;
        //Debug.Log(Origin);
        Hue = Rigid.velocity;
        Hue.Normalize();
        Hue *= radius;

        Hue = rotor * Hue;
        transform.position = (Origin + Hue);


    }

}
