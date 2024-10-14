using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementStateBase
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            ExitState(movement, movement.Crouch);
        }
        else if(movement.direction.magnitude < 0.01f)
        {
            ExitState(movement, movement.Idle);
        }

        if(movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.walkBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.walkSpeed;
        }
    }

    void ExitState(MovementStateManager movement, MovementStateBase state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
