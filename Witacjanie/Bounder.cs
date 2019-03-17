using UnityEngine;
using System.Collections;

public class Bounder : MonoBehaviour
{
    [SerializeField]
    int Vertecies = 24;
    int innerRadius = 1;
    int outerRadius = 2;
    PolygonCollider2D ThisCollider;
	// Use this for initialization
	void Start ()
    {
        ThisCollider = GetComponent<PolygonCollider2D>();
        ThisCollider.SetPath(0, MakeCircle(Vertecies, innerRadius));
        ThisCollider.SetPath(1, MakeCircle(Vertecies, outerRadius));
        
    }

    Vector2[] MakeCircle(int V, int R)
    {
        Vector2[] Circle = new Vector2[V];
        for (int i = 0; i < V; i++)
        {
            float dFi = (2f * Mathf.PI) / V;
            float x = Mathf.Cos(i * dFi) * R;
            float y = Mathf.Sin(i * dFi) * R;

            Circle[i] = new Vector2(x, y);
        }
        return Circle;
    }

}
