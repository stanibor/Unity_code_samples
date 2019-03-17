using UnityEngine;
using System.Collections;

public class GunTurn : MonoBehaviour
{

    //int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    //float camRayLength = 100f;
   

    public float rev = 30f;
    public float Maxangle = 65f;
    float rotated = 0f;

    public Transform Dad;
    public Transform Over;

    Animator Anim;

    //[SerializeField]
    ControllerMenager CMenager;

    Vector3 Helper;

    // Use this for initialization
    void Start ()
    {
        //floorMask = LayerMask.GetMask("Floor");

        Anim = GetComponentInChildren<Animator>();
        //Dad = GetComponentInParent<Transform>();
        //Debug.Log(Dad);
        CMenager = GetComponentInParent<ControllerMenager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Turn();
	}

    void Turn()
    {
        if (CMenager.Control.Moved())
        {
            Vector3 pl2M = CMenager.Control.TurnVector();
            Quaternion newRotation = Quaternion.LookRotation(pl2M, Vector3.up);
            float temp = -(newRotation.eulerAngles.y - 90f);


           // Debug.Log(temp + " " + rotated);
            // Set the player's rotation to this new rotation.
            if ((temp <= Maxangle && temp >= -Maxangle) || (temp + 180f <= Maxangle && temp + 180f >= -Maxangle))
            {
                
                if(temp >= -90f && rotated==1)
                {
                    Dad.rotation = Quaternion.Euler(25f, 0, 0);
                    rotated = 0f;

                    Helper = Quaternion.Euler(25f, 0, 0) * (Vector3.forward *2* transform.localPosition.z);
                    //Debug.Log(Helper);
                    transform.position -= Helper;
                }
                if (temp < -90f && rotated == 0)
                {
                    
                    Dad.rotation = Quaternion.Euler(-25f, 180f, 0);
                    rotated = 1f;
                    //transform.position -= transform.rotation * (Vector3.up * 2 * transform.localPosition.z);
                    //Debug.Log(transform.rotation * (Vector3.up * 2 * transform.localPosition.z));
                    Helper = Quaternion.Euler(25f, 0, 0) * (Vector3.forward * 2 * transform.localPosition.z);
                    //Debug.Log(Helper);
                    transform.position += Helper;


                }
                if(rotated == 1f)
                {
                    temp += 180;
                    temp *= -1;
                }
                //Debug.Log(temp);

                float newz = (rev * temp) / Maxangle;
                transform.rotation = Quaternion.Euler(25f+((-50f)*rotated),180f*rotated,newz);

                //Debug.Log(transform.rotation * (Vector3.up * 2 * transform.localPosition.z));

                Anim.SetFloat("Rot", newz);

                //Debug.Log(newz);

            }
                
        }
    }
}
