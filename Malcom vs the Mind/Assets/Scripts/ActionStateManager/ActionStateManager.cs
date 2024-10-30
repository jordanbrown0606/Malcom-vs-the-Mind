using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
    AudioSource _audioSource;

    [HideInInspector] public Animator anim;

    public TwoBoneIKConstraint leftHandIK;

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(Default);
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
        anim = GetComponent<Animator>();
        _audioSource = currentWeapon.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        SwitchState(Default);
    }

    public void MagOut()
    {
        _audioSource.PlayOneShot(ammo.magOutSound);
    }

    public void MagIn()
    {
        _audioSource.PlayOneShot(ammo.magInSound);
    }

    public void ChargingHandle()
    {
        _audioSource.PlayOneShot(ammo.chargingHandleSound);
    }
}
