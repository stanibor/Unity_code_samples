using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    public float DashPower = 5000f;

    public float TimeBetweenDash = 3f;
    float timer = 0f;
    bool Dashing = false;

    public Transform Target;
    Rigidbody Rigid;

    ParticleSystem Tracker;


	// Use this for initialization
	void Start ()
    {
        timer = 0f;
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
        Tracker = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (Dashing)
        {
            Tracker.Play();
            Dashing = false;
        }


        if(timer >= TimeBetweenDash)
        {
            Action();
            timer = 0;
        }
	}

    void Action()
    {
        Vector3 wzum = Target.position - transform.position;
        wzum.y = 0;
        wzum.Normalize();
        wzum *= DashPower;
        Dashing = true;
        Rigid.AddForce(wzum);
    }
}
