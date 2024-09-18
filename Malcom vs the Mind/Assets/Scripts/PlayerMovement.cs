using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxForce;
    [SerializeField] private Vector2 _move;
    private Rigidbody _rb;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        _move.x = Input.GetAxis("Horizontal");
        _move.y = Input.GetAxis("Vertical");

        Vector3 currentVelocity = _rb.velocity;
        Vector3 targetVelocity = new Vector3(_move.x, 0, _move.y) * _speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z));
        Vector3.ClampMagnitude(velocityChange, _maxForce);

        _rb.AddForce(new Vector3(velocityChange.x, 0, velocityChange.z), ForceMode.VelocityChange);

        //Vector3 offset = new Vector3(horizontal, 0, vertical);
        //transform.position += offset * speed * Time.deltaTime;
    }

}
