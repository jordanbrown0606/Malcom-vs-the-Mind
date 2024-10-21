using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipfireState hip = new HipfireState();
    public AimState aim = new AimState();

    [SerializeField] private Transform _camFollowPos;
    [SerializeField] private float _sensitivity;

    private float _xAxis, _yAxis;

    public Animator anim;
    public CinemachineVirtualCamera vCam;
    public float adsFOV = 40f;
    [HideInInspector] public float hipFOV;
    [HideInInspector] public float currentFOV;
    public float fovSmoothSpeed;

    [SerializeField] Transform aimPos;
    [SerializeField] float aimSmoothSpeed;
    [SerializeField] LayerMask aimMask;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SwitchState(hip);
        hipFOV = vCam.m_Lens.FieldOfView;
        currentFOV = hipFOV;
    }

    // Update is called once per frame
    void Update()
    {
        _xAxis += Input.GetAxisRaw("Mouse X") * _sensitivity;
        _yAxis -= Input.GetAxisRaw("Mouse Y") * _sensitivity;
        _yAxis = Mathf.Clamp(_yAxis, -60, 60);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFOV, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yAxis, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
