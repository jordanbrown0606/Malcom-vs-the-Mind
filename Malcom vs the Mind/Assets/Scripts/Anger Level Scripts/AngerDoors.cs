using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerDoors : MonoBehaviour
{

    private void Update()
    {
        if (GameManager.instance.angerKey == true)
        {
            gameObject.SetActive(false);
        }
    }
}
