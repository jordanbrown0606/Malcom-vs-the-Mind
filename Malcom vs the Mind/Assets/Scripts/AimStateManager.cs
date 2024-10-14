using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] private Transform _camFollowPos;
    [SerializeField] private float _sensitivity;

    private float _xAxis, _yAxis;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        _xAxis += Input.GetAxisRaw("Mouse X") * _sensitivity;
        _yAxis -= Input.GetAxisRaw("Mouse Y") * _sensitivity;
        _yAxis = Mathf.Clamp(_yAxis, -60, 60);
    }

    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yAxis, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xAxis, transform.eulerAngles.z);
    }
}
