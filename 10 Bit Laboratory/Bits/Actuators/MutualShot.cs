using UnityEngine;
using System.Collections;

public class MutualShot : MonoBehaviour
{

    public float TimeBetweenShoots = 3f;

    public float HoldTime = 1f;

    public bool RotateProjectile = false;

    public float ShootPower = 1200f;
    float timer = 0f;
    float Htimer = 0f;

    bool Held = false;

    public Transform Target;
    public Transform[] Rifle;

    public GameObject Projectile;
    GameObject temp;

    Vector3 HeldPos;

    Rigidbody Rigid;

    Vector3 look;


    // Use this for initialization
    void Start()
    {
        timer = 0f;
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Held)
            timer += Time.deltaTime;

        if (timer >= TimeBetweenShoots)
        {
            StartHolding();
            Action();
            timer = 0f;
        }

        if(Held)
        {
            Htimer += Time.deltaTime;
            Hold();
        }

        if(Htimer >= HoldTime && Held)
        {
            Held = false;
            Htimer = 0f;
            timer = 0f;
        }
    }

    void StartHolding()
    {
        Held = true;
        HeldPos = transform.position;
    }

    void Hold()
    {
        //Rigid.MovePosition(HeldPos);
        transform.position = HeldPos;
        Debug.Log(HeldPos);
    }

    void Action()
    {
        for(int i=0;i<Rifle.Length;i++)
        {
            temp = (GameObject)Instantiate(Projectile, Rifle[i].position, Quaternion.Euler(Vector3.zero));
            if (RotateProjectile)
                temp.transform.rotation = Rifle[i].rotation;
            temp.GetComponent<Rigidbody>().AddForce(Rifle[i].rotation * Vector3.forward * ShootPower);
        }

    }
}
