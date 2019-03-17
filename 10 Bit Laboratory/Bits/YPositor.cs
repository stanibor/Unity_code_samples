using UnityEngine;
using System.Collections;

public class YPositor : MonoBehaviour
{
    public float Height = 0;
    public float smoothing = 0;

    Vector3 NewPos;
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.y != Height)
        {
            NewPos.x = transform.position.x;
            NewPos.z = transform.position.z;
            NewPos.y = Height;

            transform.position = Vector3.Lerp(transform.position, NewPos, smoothing * Time.deltaTime);
        }	
	}
}
