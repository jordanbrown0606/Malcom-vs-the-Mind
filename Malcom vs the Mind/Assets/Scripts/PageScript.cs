using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScript : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _notePanel;
    [SerializeField] GameObject _crosshair;
    public void Pickup(GameObject interactor)
    {
        _notePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        interactor.SetActive(false);
        _crosshair.SetActive(false);
        GameManager.instance.hasNote = true;
    }

    private void Start()
    {
        GameManager.instance.hasNote = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
