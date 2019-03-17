using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.

    public Vector3 LTCorner;
    public Vector3 RBCorner;

    Vector3 WidthUnits;
    Vector3 HeightUnits;

    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;

        WidthUnits = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2, 0)) - Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
        Debug.Log(WidthUnits);
        HeightUnits = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2,0, 0)) - Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
        HeightUnits = Quaternion.Euler(90, 0, 0) * HeightUnits;
        Debug.Log(HeightUnits);
        //Debug.Log(transform.position.z);
        LTCorner -= WidthUnits;
        LTCorner += HeightUnits;
        RBCorner += WidthUnits;
        RBCorner += HeightUnits;
        //LTCorner += transform.position;
        //RBCorner += transform.position;
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.x < LTCorner.x)
            transform.position -= Vector3.right * (transform.position.x - LTCorner.x);
        if (transform.position.x > RBCorner.x)
            transform.position -= Vector3.right * (transform.position.x - RBCorner.x);

       /* if (transform.position.z > LTCorner.z)
            transform.position -= Vector3.forward * (transform.position.z - LTCorner.z);
        if (transform.position.z < RBCorner.z)
            transform.position -= Vector3.forward * (transform.position.z - RBCorner.z);
       */ 
    }

}
