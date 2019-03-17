using UnityEngine;
using System.Collections;

public class LowerMenager : MonoBehaviour
{

    public void Begin()
    {
        Application.LoadLevel(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
