using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    float baseSpeed = 5f;

    public Collider movementCollider { get; protected set; }
    public Rigidbody movementRigidbody { get; protected set; }

    Vector3 mover;
    
	// Use this for initialization
	void Start ()
    {

        mover = Vector3.zero;

        movementCollider = GetComponent<Collider>();
        movementRigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        mover = Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right;
        mover = (mover.magnitude > 1f ? mover.normalized : mover);
        mover *= baseSpeed * Time.fixedDeltaTime;
        //rotator = Input.GetAxis("Mouse X") * Vector3.up;
    }

    void FixedUpdate()
    {
        Move(mover);
    }

    void Move(Vector3 move)
    {
        transform.Translate(move);
    }
}
