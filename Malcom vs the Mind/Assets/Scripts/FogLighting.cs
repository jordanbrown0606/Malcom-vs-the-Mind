using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogLighting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogDensity = 0.02f;
    }
}
