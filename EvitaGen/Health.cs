using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField] UnityEvent onOutOfHealth;
    [SerializeField] UnityEvent onOutOfAmmo;
    [SerializeField] UnityEvent onSwitch;
    [SerializeField] UnityEvent onChange;

    public UnityEvent OnOutOfAmmo {
        get { return onOutOfAmmo; }
    }

    public UnityEvent OnOutOfHealth {
        get { return onOutOfHealth; }
    }

    public UnityEvent OnSwitch {
        get { return onSwitch; }
    }

    public UnityEvent OnChange {
        get { return onChange; }
    }

    [SerializeField] int maxResource = 10;

    public bool isWhite { get; private set; }

    public bool isBlack {
        get { return !isWhite; }
    }

    public int whiteResource { get; private set; }
    public int blackResource { get; private set; }

    public int ammoResource {
        get { return isWhite ? blackResource : whiteResource; }
    }

    public int healthResource {
        get { return isWhite ? whiteResource : blackResource; }
    }

    void Awake() {
        Reset();
    }

    public void Reset() {
        isWhite = true;
        whiteResource = maxResource;
        blackResource = maxResource;
    }

    public bool TakeBullet(bool isBulletWhite, int damage = 1) {
        if (isBulletWhite != isWhite) {
            whiteResource -= isWhite ? 2 * damage : 0;
            blackResource -= !isWhite ? 2 * damage : 0;

            if ((isWhite && whiteResource < 0) || (!isWhite && blackResource < 0))
                onOutOfHealth.Invoke();

            GetComponent<PlayerSoundSource>().PlayHitSound();

            OnChange.Invoke();
            return true;
        }
        else {
            whiteResource += isWhite ? damage : 0;
            blackResource += !isWhite ? damage : 0;

            whiteResource = whiteResource > maxResource ? maxResource : whiteResource;
            blackResource = blackResource > maxResource ? maxResource : blackResource;

            GetComponent<PlayerSoundSource>().PlayHitPositiveSound();

            OnChange.Invoke();
        }

        return false;
    }

    public void TakeAmmo(int ammo = 1) {
        whiteResource -= !isWhite ? ammo : 0;
        blackResource -= isWhite ? ammo : 0;

        if ((!isWhite && whiteResource <= 0) || (isWhite && blackResource <= 0))
            OnOutOfAmmo.Invoke();

        OnChange.Invoke();
    }

    public void Switch() {
        isWhite = !isWhite;

        /*if ((isWhite && whiteResource <= 0) || (!isWhite && blackResource <= 0))
            onOutOfHealth.Invoke();*/

        OnSwitch.Invoke();
        OnChange.Invoke();
    }
}