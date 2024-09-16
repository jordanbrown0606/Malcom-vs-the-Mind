using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
            Debug.DrawRay(transform.position, forward, Color.red, 5f);

            if (Physics.Raycast(ray, out hit))
            {
                hit.transform.gameObject.GetComponent<IDamageable>()?.TakeDamage(2);
            }
        }
    }
}
