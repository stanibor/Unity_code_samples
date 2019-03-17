using UnityEngine;
using System.Collections;

public class EthericDash : MonoBehaviour
{
    public float DashPower = 5000f;

    public float TimeBetweenDash = 3f;
    public float Treshold = 0.3f;
    public float HoldTime = 1f;

    public bool DonutMode;

    int WallMask;
    float range = 100f;
    RaycastHit shootHit;
    Ray BackRay;

    float timer = 0f;
    bool Dashing = false;

    float Htimer = 0f;

    bool Held = false;

    string Tagg = "Bit";

    public Transform Target;
    Rigidbody Rigid;
    SphereCollider Matter;

    ParticleSystem Tracker;

    Vector3 HeldPos;
    Vector3 RemPos;

    SpriteRenderer rend;

    void OnTriggerEnter(Collider other)
    {
        if(Dashing && DonutMode)
        {
            if(other.tag == "Wall")
            {
                BackRay.origin = transform.position;
                BackRay.direction = -Rigid.velocity;

                if (Physics.Raycast(BackRay, out shootHit, range, WallMask))
                {
                    Debug.Log(shootHit.point);
                    transform.position = shootHit.point;
                }
                else
                {
                    Matterialize();
                }
            }
        }
    }




    // Use this for initialization
    void Start()
    {
        WallMask = LayerMask.GetMask("Wall");

        rend = GetComponentInChildren<SpriteRenderer>();

        Tagg = tag;
        timer = 0f;
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
        Tracker = GetComponent<ParticleSystem>();
        Matter = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Held)
            timer += Time.deltaTime;

        if (Dashing)
        {
            Tracker.Play();
            //Dashing = false;
        }

        if (Rigid.velocity.magnitude <= Treshold && Dashing)
        {
            Dashing = false;
            Matterialize();
            timer = 0f;
        }


        if (timer >= TimeBetweenDash)
        {
            StartHolding();
            //Action();
            timer = 0f;
        }

        if (Held)
        {
            Htimer += Time.deltaTime;
            Hold();
        }

        if (Htimer >= HoldTime)
        {
            Action();
            timer = 0f;
            Held = false;
            Htimer = 0f;
        }
    }

    void Action()
    {
        Vector3 wzum = RemPos - transform.position;
        wzum.y = 0f;
        wzum.Normalize();
        wzum *= DashPower;
        Dashing = true;
        Rigid.AddForce(wzum);
        Etherize();
    }

    void Etherize()
    {
        tag = "Etheric";
        Matter.enabled = false;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0.6f);
    }

    void Matterialize()
    {
        tag = Tagg;
        Matter.enabled = true;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1f);
    }

    void StartHolding()
    {
        Held = true;
        HeldPos = transform.position;
        RemPos = Target.position;
    }

    void Hold()
    {
        Rigid.MovePosition(HeldPos);
        Etherize();
    }
}
