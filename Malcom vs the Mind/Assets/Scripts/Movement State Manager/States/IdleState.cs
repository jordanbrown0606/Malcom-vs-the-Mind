using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementStateBase
{
    public override void EnterState(MovementStateManager movement)
    {
        
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.direction.magnitude > 0.01f)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                movement.SwitchState(movement.Run);
            }
            else
            {
                movement.SwitchState(movement.Walk);
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            movement.SwitchState(movement.Crouch);
        }
    }
}
