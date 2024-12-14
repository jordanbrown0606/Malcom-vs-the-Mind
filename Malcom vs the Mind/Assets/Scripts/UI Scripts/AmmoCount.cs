using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoCount : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private TextMeshProUGUI _weaponCountText;
    private WeaponAmmo _weaponAmmo;

    // Start is called before the first frame update
    void Start()
    {
        _weaponAmmo = _weapon.GetComponent<WeaponAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        _weaponCountText.text = $"<b>{_weaponAmmo.curAmmo}</b> / <b>{_weaponAmmo.extraAmmo}</b>";
    }
}
