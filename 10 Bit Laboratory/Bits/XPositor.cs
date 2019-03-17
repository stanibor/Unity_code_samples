using UnityEngine;
using System.Collections;

public class XPositor : MonoBehaviour
{
    public Vector3 NewPos;
    public float smoothing = 0;

    //Vector3 NewPos;
    // Update is called once per frame
    void Update()
    {
            transform.position = Vector3.Lerp(transform.position, NewPos, smoothing * Time.deltaTime);
    }
}
