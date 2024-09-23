using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    [SerializeField] private Transform _lookAt;

    [SerializeField] private float distance = 10.0f;

    [SerializeField] private Transform _shootPosition;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity;

    // Update is called once per frame
    void LateUpdate()
    {

        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY -= Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = _lookAt.position + rotation * Direction;

        transform.LookAt(_lookAt.position);

        if(Input.GetMouseButtonDown(1))
        {
            _lookAt = _shootPosition;
        }



    }
}
