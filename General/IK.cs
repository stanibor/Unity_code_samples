using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IK : MonoBehaviour
{
    [Serializable]
    public class Chain
    {
        [SerializeField]
        Transform upper;
        [SerializeField]
        Transform lower;
        [SerializeField]
        Transform end;

        public bool isReversed = false;

        public Transform upperBone
        { get { return upper; } }
        public Transform lowerBone
        { get { return lower; } }
        public Transform endBone
        { get { return end; } }

        public Chain(Transform a, Transform b, Transform c = null)
        {
            this.upper = a;
            this.lower = b;
            this.end = c;
        }

        public void setChain(Transform upper,Transform lower, Transform end = null)
        {
            this.upper = upper;
            this.lower = lower;
            this.end = end;
        }
        public void solve(Vector3 goal, float swivelAngle = 0f)
        {
            float dA, dB;
            float x, y, z, h;
            float thetaA, thetaB, thetaC;

            Vector3 hd = goal - upper.position;
            x = hd.x;
            y = hd.y;
            z = hd.z;

            dA = Vector3.Distance(upper.position, lower.position);
            dB = (end == null) ? Vector3.Distance(lower.position, goal) : Vector3.Distance(lower.position, end.position);
            h = Vector3.Distance(upper.position, goal);


            thetaC = x > 0f ? Mathf.Atan2(-z, x) : Mathf.Atan2(-z, x)-Mathf.PI;

            thetaA = (h * h - dA * dA - dB * dB) / (2 * dA * dB);


            thetaA = Mathf.Acos(thetaA);

            thetaA = isReversed ? -thetaA : thetaA;
            thetaA = (x > 0f) ? thetaA : -thetaA;

            thetaA = h > (dA + dB) ? 0f : thetaA;

            x = hd.x * (dA + dB * Mathf.Cos(thetaA)) + hd.y * (dB * Mathf.Sin(thetaA));
            y = hd.y * (dA + dB * Mathf.Cos(thetaA)) - hd.x * (dB * Mathf.Sin(thetaA));

            thetaB = Mathf.Atan2(y, x);


            upper.rotation = Quaternion.Euler(swivelAngle, thetaC * Mathf.Rad2Deg, thetaB * Mathf.Rad2Deg);
            lower.localRotation = Quaternion.Euler(0f, 0f, thetaA * Mathf.Rad2Deg);
        }
        public void drawGizmos(Vector3 goal)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(upper.position, lower.position);
            Gizmos.DrawLine(lower.position, goal);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(upper.position, goal);
        }

    }


    [SerializeField]
    Chain chain;

    [SerializeField]
    Transform goal;

    [SerializeField]
    float swivel = 0f;

    void FixedUpdate()
    {
        if(chain != null && goal != null)
        {
            chain.solve(goal.position, swivel);
        }
    }

    void OnDrawGizmos()
    {
        if (chain != null && goal != null)
            chain.drawGizmos(goal.position);
    }
}
