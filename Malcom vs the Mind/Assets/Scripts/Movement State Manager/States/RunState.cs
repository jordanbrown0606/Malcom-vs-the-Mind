using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementStateBase
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Walk);
        }
        else if(movement.direction.magnitude < 0.01f)
        {
            ExitState(movement, movement.Idle);
        }

        if (movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.runBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.runSpeed;
        }
    }

    void ExitState(MovementStateManager movement, MovementStateBase state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
