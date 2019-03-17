using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunHandler : MonoBehaviour
{
    public float MaxLoad = 10000f;
    public float timeBetweenAlts = 5f;
    public float CannonPower = 50f;
    public float LperS = 5f;
    public GameObject Bullet;
    public GameObject AltBullet;

    public Animator Anim;

    public Slider Load;
    public Slider Ammo;

    float load = 0f;
    float Cumulation = 0f;
    

    float Alter = 0f;
    GameObject temp;

    bool Loading = false;

    ControllerMenager CMenager;

    void Start()
    {
        load = MaxLoad;
        CMenager = GetComponentInParent<ControllerMenager>();
    }
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Load: " + load);

        Load.value = load / MaxLoad;
        Ammo.value = Alter / timeBetweenAlts;


        if(load < MaxLoad)
            load += Time.deltaTime * LperS;
        Alter += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (CMenager.Control.ShootAxis()>=0.8f && load > (MaxLoad/10f))
        {
            Loading = true;
        }

        if(Loading)
        {
            Cumulate();
        }

        if(Loading && (CMenager.Control.ShootAxis() == 0f || load <= 0))
        {
            Shoot();
        }

        if (CMenager.Control.SpawnAxis() >= 0.8f && Alter >= timeBetweenAlts)
        {
            // ... shoot the gun.
            AltShoot();
        }
    }

    void Cumulate()
    {
        Cumulation += Time.deltaTime * 3 * LperS;
        load -= Time.deltaTime * 3 * LperS;
    }

    void Shoot()
    {
        temp = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * CannonPower);
        if (temp.GetComponent<Beamer>() != null)
            temp.GetComponent<Beamer>().AcumulatedPower = Cumulation * (temp.GetComponent<Beamer>().MaxPower/MaxLoad);
        Cumulation = 0f;
        Loading = false;
        Anim.SetTrigger("Shoot");
        //timer = 0f;
    }

    void AltShoot()
    {
        temp = (GameObject)Instantiate(AltBullet, transform.position, Quaternion.Euler(Vector3.zero));
        temp.GetComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward  * CannonPower);

        Alter = 0f;
        Anim.SetTrigger("Shoot");
    }
}
