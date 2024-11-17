using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") == false)
        {
            other.GetComponent<IDamageable>()?.TakeDamage(2);
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
