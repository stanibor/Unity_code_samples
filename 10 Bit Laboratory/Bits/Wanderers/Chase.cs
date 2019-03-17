using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour
{
    public float speed = 0.2f;

    public Transform Target;
    Rigidbody Rigid;

    Vector3 wzu;
    // Use this for initialization
    void Start ()
    {
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Action();
	}

    void Action()
    {
        wzu = Target.position - transform.position;
        wzu.Normalize();
        wzu *= speed;

        Rigid.MovePosition(transform.position + wzu);
    }
}
