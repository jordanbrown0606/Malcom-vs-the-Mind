using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light[] _lights;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.angerKey == true)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].color = Color.red;
            }
        }
    }
}
