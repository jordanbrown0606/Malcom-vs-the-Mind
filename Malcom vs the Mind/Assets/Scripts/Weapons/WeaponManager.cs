using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float _fireRate;
    [SerializeField] bool _semiAuto;
    private float _fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _barrelPos;
    [SerializeField] private float _bulletVelocity;
    [SerializeField] private int _bulletsPerShot;
    AimStateManager _aim;

    [SerializeField] AudioClip _gunShot;
    AudioSource _audioSource;
    WeaponAmmo _ammo;
    ActionStateManager _actions;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _fireRateTimer = _fireRate;
        _aim = GetComponentInParent<AimStateManager>();
        _ammo = GetComponent<WeaponAmmo>();
        _actions = GetComponentInParent<ActionStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire()) Fire();
    }

    private bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;

        if(_fireRateTimer < _fireRate)
        {
            return false;
        }
        if (_ammo.curAmmo == 0)
        {
            return false;
        }
        if(_actions.currentState == _actions.Reload)
        {
            return false;
        }

        if ( _semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        else if(!_semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    private void Fire()
    {
        _fireRateTimer = 0;
        _barrelPos.LookAt(_aim.aimPos);
        _audioSource.PlayOneShot(_gunShot);
        _ammo.curAmmo--;
        for (int i = 0; i < _bulletsPerShot; i++)
        {
            GameObject curBullet = Instantiate(_bullet, _barrelPos.position, _barrelPos.rotation);
            Rigidbody rb = curBullet.GetComponent<Rigidbody>();
            rb.AddForce(_barrelPos.forward * _bulletVelocity, ForceMode.Impulse);
        }
    }
}
