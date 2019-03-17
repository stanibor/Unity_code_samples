using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalKeeper : MonoBehaviour {
    public float Smoothing;

    void FixedUpdate()
    {
        Vector3 Vertical = -transform.position;
        Vertical.Normalize();

        float angle = Mathf.Atan2(-Vertical.x, Vertical.y) * 180f / Mathf.PI;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), Smoothing * Time.fixedDeltaTime);
    }
}
