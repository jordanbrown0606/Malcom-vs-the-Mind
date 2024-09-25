using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] Cinemachine.AxisState _xAxis, _yAxis;
    [SerializeField] private Transform _camFollowPos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        _xAxis.Update(Time.deltaTime);
        _yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yAxis.Value, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xAxis.Value, transform.eulerAngles.z);
    }
}
