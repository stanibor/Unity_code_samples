using UnityEngine;
using System.Collections;

public class SemiRandomTrack : MonoBehaviour
{

    public float speed = 0.05f;
    public float Density = 5f;
    public float NoReplyTime = 1f;

    public Transform Target;
    Rigidbody Rigid;

    float timer = 0f;

    Vector3 wzu;
    Vector3 Destination;
    Vector3 ProbDest;

    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
        GenerateDirection();
    }

    void GenerateDirection()
    {
        Destination = new Vector3(Random.value - Random.value, 0, Random.value - Random.value);

        Destination = Destination.normalized * Density + transform.position;
 
        Destination -= Target.position;

        Vector3 Temp = transform.position - Target.position; //Vector3.up * transform.position.y;
        if (Destination.magnitude > Temp.magnitude)
            Destination = Temp;

        Destination += Target.position;

        Debug.Log("DEst: " + Destination);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= NoReplyTime)
        {
            GenerateDirection();
            timer = 0f;
        }
        Action();
    }

    void Action()
    {
        wzu = Destination - transform.position;
        Debug.Log("Wzu: " + wzu);
        if (wzu.magnitude < 0.1f)
            GenerateDirection();
        else
            timer += Time.deltaTime;
        wzu.Normalize();
        wzu *= speed;

        Rigid.MovePosition(transform.position + wzu);
    }
}
