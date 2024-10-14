using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementStateBase
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Crouching", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(movement.direction.magnitude < 0.01f)
            {
                ExitState(movement, movement.Idle);
            }
            else
            {
                ExitState(movement, movement.Walk);
            }

            if (movement.vInput < 0)
            {
                movement.currentMoveSpeed = movement.crouchBackSpeed;
            }
            else
            {
                movement.currentMoveSpeed = movement.crouchSpeed;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            ExitState(movement, movement.Walk);
        }
    }

    void ExitState(MovementStateManager movement, MovementStateBase state)
    {
        movement.anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}
