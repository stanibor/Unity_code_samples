using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public Vector3 rot;

    Vector3 temp;

    void Update()
    {
        temp = Quaternion.ToEulerAngles(transform.rotation) * (360f/(2* Mathf.PI));

        Debug.Log("Temp: "+ temp);
        transform.rotation = Quaternion.Euler(temp + rot);
    }

}
