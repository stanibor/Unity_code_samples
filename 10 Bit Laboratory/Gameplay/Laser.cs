using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    CapsuleCollider collid;
    LineRenderer Beamer;
    public Transform Beamend;

    public float PushBackPower = 50f;
    public float DamagePerSec = 10f;

    PlayerHealth Loser;
    Rigidbody Dloser;
    bool lasered = false;

    float range = 100f;
    RaycastHit shootHit;
    Ray WallRay;
    int WallMask;
    int DynaMask;


    Vector3 helper;

    // Use this for initialization
    void Start ()
    {
        collid = GetComponent<CapsuleCollider>();
        Beamer = GetComponent<LineRenderer>();

        WallMask = LayerMask.GetMask("Wall");
        DynaMask = LayerMask.GetMask("Dynamic");

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        WallRay.origin = transform.position;
        WallRay.direction = transform.rotation * Vector3.forward;

        if (Physics.Raycast(WallRay, out shootHit, range, DynaMask))
        {
            SetBeam(shootHit.point);
            if (shootHit.collider.tag == "Player")
            {
                Loser = shootHit.collider.GetComponent<PlayerHealth>();
                Loser.TakeDamage(DamagePerSec*Time.deltaTime);
            }
            if(shootHit.collider.tag != "Etheric")
            {
                helper = WallRay.direction.normalized;
                helper *= PushBackPower;
                Dloser = shootHit.collider.GetComponent<Rigidbody>();
                Dloser.AddForce(helper);
            }


        }
        else if (Physics.Raycast(WallRay, out shootHit, range, WallMask))
        {
            SetBeam(shootHit.point);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Loser = shootHit.collider.GetComponent<PlayerHealth>();
            Loser.TakeDamage(DamagePerSec/3);
        }
    }

    void SetBeam(Vector3 end)
    {
        Beamend.transform.position = end;
        helper = transform.position - end;
        collid.center = new Vector3(collid.center.x, collid.center.y, helper.magnitude / 2);
        collid.height = helper.magnitude;

        Beamer.SetPosition(0, transform.position);
        Beamer.SetPosition(1, end);
    }
}
