using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {
    public GameObject TargetPortal;

    private bool _colliding = false;
    private GameObject _collidingObject;
    private Portal portal, targetPortal;
    Transform PortalExit, TargetPortalEnter, PortalEnter, TargetPortalExit;

    int counter = 0;

    void Start() {
        portal = GetComponentInParent<Portal>();
        TargetPortal = portal.TargetPortal.gameObject;
        targetPortal = TargetPortal.GetComponentInParent<Portal>();

        PortalExit = portal.Exit;
        PortalEnter = portal.TargetPortal.transform;
        TargetPortalExit = targetPortal.Exit;
        TargetPortalEnter = portal.transform;
    }

    void FixedUpdate() {
        if (_colliding) {
           Vector3 portalToObject = _collidingObject.transform.position - portal.transform.position;
           float dotProduct = Vector3.Dot(portal.transform.forward, portalToObject);

            Debug.Log("Dot Product: " + dotProduct.ToString());

           if (dotProduct < 0f)
            {
                Vector3 playerOffsetFromPortal = TargetPortalEnter.transform.InverseTransformPoint(_collidingObject.transform.position);
                _collidingObject.transform.position = TargetPortalExit.TransformPoint(playerOffsetFromPortal);

                Quaternion newRot = TargetPortalExit.rotation * Quaternion.Inverse(TargetPortalEnter.rotation) * _collidingObject.transform.rotation ;
                _collidingObject.transform.rotation = newRot;


                Debug.Log(++counter);
            }
           _colliding = false;
        }
        else
        {
            counter = 0;
        }
    }

    void OnTriggerEnter(Collider other) {
        _collidingObject = other.gameObject;
        if (_collidingObject.GetComponent<Interact>() != null)
        {
            //_colliding = true;
            Vector3 portalToObject = _collidingObject.transform.position - portal.transform.position;
            float dotProduct = Vector3.Dot(portal.transform.forward, portalToObject);
            Debug.Log("Dot Product: " + dotProduct.ToString());

            if (dotProduct < 0f)
            {
                Teleport(_collidingObject);
            }
        }
            
    }

    void OnTriggerExit(Collider other) {
        if (_collidingObject.GetComponent<Interact>() != null)
            _colliding = false;
    }

    void Teleport(GameObject _collidingObject)
    {
        Vector3 playerOffsetFromPortal = TargetPortalEnter.transform.InverseTransformPoint(_collidingObject.transform.position);
        _collidingObject.transform.position = TargetPortalExit.TransformPoint(playerOffsetFromPortal);

        Quaternion newRot = TargetPortalExit.rotation * Quaternion.Inverse(TargetPortalEnter.rotation) * _collidingObject.transform.rotation;
        _collidingObject.transform.rotation = newRot;


        Debug.Log(++counter);
    }
}

