using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _groundOffset;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _gravity = -9.81f;

    private Vector3 _direction;
    private Vector3 _spherePos;
    private Vector3 _velocity;
    private float _hzInput, _vInput;
    private CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionAndMove();
        Gravity();
    }

    private void DirectionAndMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        _direction = transform.forward * _vInput + transform.right * _hzInput;

        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    private bool isGrounded()
    {
        _spherePos = new Vector3(transform.position.x, transform.position.y - _groundOffset, transform.position.z);
        if(Physics.CheckSphere(_spherePos, _controller.radius - 0.05f, _groundMask))
        {
            return true;
        }
        return false;
    }

    private void Gravity()
    {
        if(!isGrounded())
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else if(_velocity.y < 0)
        {
            _velocity.y = -2;
        }

        _controller.Move(_velocity * Time.deltaTime);
    }
}
