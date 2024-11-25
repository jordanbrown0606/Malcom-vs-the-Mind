using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask aimMask;

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            if(hit.transform.gameObject.GetComponent<IInteractable>() != null && Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.gameObject.GetComponent<IInteractable>().Pickup(gameObject);
            }
            else
            {
                return;
            }
        }
    }
}
