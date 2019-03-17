using UnityEngine;
using System.Collections;

public class ControllerMenager : MonoBehaviour
{
    [SerializeField]
    bool Joystick = false;

    IController control;
    public IController Control
    {
        get { return control; }
    }

	void Awake ()
    {
        if (Joystick)
            control = new JoystickController();
        else
            control = new MouseController();
        if (control != null)
            control.Affected = transform;
	}
}
