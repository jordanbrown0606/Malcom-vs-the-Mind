using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    [HideInInspector] public float hzInput, vInput;
    [HideInInspector] public Vector3 direction;
    private CharacterController _controller;
    public float currentMoveSpeed;
    public float walkSpeed, walkBackSpeed;
    public float runSpeed, runBackSpeed;
    public float crouchSpeed, crouchBackSpeed;
    #endregion

    #region Gravity
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 _velocity;
    #endregion

    #region GroundCheck
    [SerializeField] private float _groundOffset;
    [SerializeField] private LayerMask _groundMask;
    private Vector3 _spherePos;
    #endregion

    MovementStateBase currState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        currState.UpdateState(this);
    }

    public void SwitchState(MovementStateBase state)
    {
        currState = state;
        currState.EnterState(this);
    }

    private void DirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        direction = transform.forward * vInput + transform.right * hzInput;

        _controller.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
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
