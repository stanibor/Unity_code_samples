using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    [SerializeField]
    Color allColor;

    [SerializeField]
    Color secoundaryColor;

    [SerializeField]
    bool hasEndColor = false;

    public Color color
    {
        get { return allColor; }
        set { setColor(value); }
    }

    public Color endColor
    {
        get { return secoundaryColor; }
        set { setEndColor(value); }
    }

    [SerializeField]
    List<SpriteRenderer> sprites;
    [SerializeField]
    List<TrailRenderer> trails;
    [SerializeField]
    List<ParticleSystem> particles;
    [SerializeField]
    List<Colorizer> colorizers;

    public void SwapColors()
    {
        Color temp = allColor;

        setColor(secoundaryColor);
        setEndColor(temp);
    }

    // Use this for initialization
    void Start ()
    {
        setColor(allColor);
        if (hasEndColor)
            setEndColor(secoundaryColor);
	}

    void setColor(Color newColor)
    {
        allColor = newColor;

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = allColor;
        }
        foreach (TrailRenderer trail in trails)
        {
            trail.startColor = allColor;
        }
        foreach (ParticleSystem particle in particles)
        {
            particle.startColor = allColor;
        }
        foreach (Colorizer colorizer in colorizers)
        {
            colorizer.color = allColor;
        }

        if (!hasEndColor)
            setEndColor(allColor);
    }

    void setEndColor(Color newColor)
    {
        secoundaryColor = newColor;

        foreach (TrailRenderer trail in trails)
        {
            trail.endColor = secoundaryColor;
        }
        foreach (Colorizer colorizer in colorizers)
        {
            colorizer.color = secoundaryColor;
        }
    }


}
