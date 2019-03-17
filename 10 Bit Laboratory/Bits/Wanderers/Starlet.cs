using UnityEngine;
using System.Collections;

public class Starlet : MonoBehaviour
{
    public float speed = 0.05f;
    public float DistanceKeep = 5f;

    Vector3[] Dots;

    public Transform Target;
    Rigidbody Rigid;



    Vector3 wzu;
    Vector3 Destination;
    Vector3 temp;
    Vector3 Minim;

    void Init()
    {
        Dots = new Vector3[9];
        Dots[0] = Vector3.zero;
        Dots[1] = new Vector3(1, 0, 0);
        Dots[2] = new Vector3(1, 0, 1); 
        Dots[3] = new Vector3(0, 0, 1);
        Dots[4] = new Vector3(-1, 0, 1); 
        Dots[5] = new Vector3(-1, 0, 0);
        Dots[6] = new Vector3(-1, 0, -1); 
        Dots[7] = new Vector3(0, 0, -1);
        Dots[8] = new Vector3(1, 0, -1);

    }

    void DistUpdate()
    {
        for(int i=0; i<Dots.Length; i++)
        {
            Dots[i].Normalize();
            Dots[i] *= DistanceKeep;
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
        DistUpdate();
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
        GenerateDirection();
    }

    void GenerateDirection()
    {
        Destination = Target.position + Dots[0];
        for(int i = 0; i < Dots.Length; i++)
        {
            temp = Target.position + Dots[i];
            temp -= transform.position;

            Minim = Destination - transform.position;

            if (Minim.magnitude > temp.magnitude)
                Destination = temp + transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }

    void Action()
    {
        wzu = Destination - transform.position;
        //Debug.Log("Wzu: " + wzu);
        GenerateDirection();
        wzu.Normalize();
        wzu *= speed;

        Rigid.MovePosition(transform.position + wzu);
    }
}
