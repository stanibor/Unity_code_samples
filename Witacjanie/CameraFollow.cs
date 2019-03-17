using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform World;

    public List<Transform> targets;

    public float Smoothing;
    public Vector3 offset;

    public float maxZoom;
    public float minZoom;
    public float zoomLimiter;

    Camera cam;

    

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Move();
        Rotate();
        Zoom();

        
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPos = centerPoint + offset;

        transform.position = Vector3.Lerp(transform.position, newPos, Smoothing);
    }

    void Rotate()
    {
        //Quaternion newRotation = Quaternion.Euler(0,0,GetAverageRotation());

        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Smoothing);
        if (World != null)
            transform.rotation = World.transform.rotation;
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = newZoom;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].position;


        return GetBounds().center;
    }

    float GetGreatestDistance()
    {
        if (targets.Count <= 1)
            return 0;
        Bounds bounds = GetBounds();

        //return Mathf.Max(bounds.size.x, bounds.size.y);
        return bounds.size.magnitude;
    }

    float GetAverageRotation()
    {
        float angle = 0;
        foreach (Transform target in targets)
        {
            angle += target.rotation.eulerAngles.z;
        }

        return angle / targets.Count;
    }

    Bounds GetBounds()
    {
        Bounds bounds = new Bounds();

        foreach (Transform target in targets)
        {
            if(target != null)
                bounds.Encapsulate(target.position);
        }

        return bounds;
    }
}
