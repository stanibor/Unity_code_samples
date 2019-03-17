using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public UnityEvent OnMenu;
    public UnityEvent OnPadMenu;

    public RectTransform PadPanel;
    public RectTransform MainMenu;

    public void BackToMenu()
    {
        PadPanel.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        OnMenu.Invoke();
    }

    public void PadSelectMenu()
    {
        PadPanel.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        OnPadMenu.Invoke();
    }

    public void Quit()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }
}
