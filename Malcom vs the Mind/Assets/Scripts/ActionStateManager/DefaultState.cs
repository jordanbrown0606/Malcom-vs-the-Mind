using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.leftHandIK.weight = 1.0f;
    }

    public override void UpdateState(ActionStateManager actions)
    {
        actions.leftHandIK.weight = Mathf.Lerp(actions.leftHandIK.weight, 1, 10 * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }
    }

    bool CanReload(ActionStateManager action)
    {
        if(action.ammo.curAmmo == action.ammo.magazineSize)
        {
            return false;
        }
        else if(action.ammo.extraAmmo == 0) return false;
        else return true;
    }
}
