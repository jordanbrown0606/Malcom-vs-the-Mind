using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;

    private bool _iterateEnemies = true;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.angerKey == true && _iterateEnemies == true)
        {
            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].GetComponent<Agent>().enabled = true;
            }

            _iterateEnemies = false;
        }
    }
}
