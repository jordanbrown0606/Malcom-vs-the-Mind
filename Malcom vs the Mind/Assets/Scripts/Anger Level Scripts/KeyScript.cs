using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour, IInteractable
{
    public void Pickup(GameObject interactor)
    {
        GameManager.instance.angerKey = true;
        gameObject.SetActive(false);
    }
}
