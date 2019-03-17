using UnityEngine;
using System.Collections;

public class ZRun : MonoBehaviour
{
    public float speed = 0.1f;

    public Transform Target;
    Rigidbody Rigid;

    Vector3 wzu;
    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }

    void Action()
    {
        wzu.y = 0;
        wzu.z = Target.position.z - transform.position.z;

        wzu.x = (Target.position.x - transform.position.x)*Random.value;

        wzu.Normalize();
        wzu *= speed;

        Rigid.MovePosition(transform.position + wzu);
    }
}
