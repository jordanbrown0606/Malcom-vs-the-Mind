using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    [SerializeField] private Transform _movePosition;
    [SerializeField] private int _speed;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.hasNote == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _movePosition.position, _speed * Time.deltaTime);
        }
    }
}
