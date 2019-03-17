using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

    public float TimeBetweenShoots = 3f;
    public float Smoothing = 3f;

    public float ShootPower = 1200f;
    float timer = 0f;

    public Transform Target;

    public Transform Pivot;
    public Transform Rifle;

    public GameObject Projectile;
    GameObject temp;

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
        timer += Time.deltaTime;

        look = Target.position - Pivot.position;
        look.y = 0; //Pivot.position.y;
        Quaternion newRotation = Quaternion.LookRotation(look, Vector3.up);


        Pivot.rotation = Quaternion.Lerp(Pivot.rotation, newRotation, Smoothing * Time.deltaTime);



        if (timer >= TimeBetweenShoots)
        {
            Action();
            timer = 0;
        }
    }

    void Action()
    {
        temp = (GameObject)Instantiate(Projectile, Rifle.position, Quaternion.Euler(Vector3.zero));
        temp.GetComponent<Rigidbody>().AddForce(Rifle.rotation * Vector3.forward * ShootPower);
    }
}
