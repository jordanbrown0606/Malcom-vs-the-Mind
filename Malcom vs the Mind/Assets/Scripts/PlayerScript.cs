using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderFill;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _slider.value = _health;

        if (_slider.value <= 3)
        {
            _sliderFill.color = Color.red;
        }
    }

    private void Die()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0)
        {
            Die();
        }
    }
}
