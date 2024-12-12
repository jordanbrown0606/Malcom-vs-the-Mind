using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private AudioSource _source;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        _source = GetComponent<AudioSource>();
        _source.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") == false)
        {
            other.GetComponent<IDamageable>()?.TakeDamage(2);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Update()
    {
        StartCoroutine(DestroyObject());   
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
