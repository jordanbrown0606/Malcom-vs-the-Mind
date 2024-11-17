using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Idle : StateBase
    {
        public override StateType GetStateType { get { return StateType.Idle; } }

        public override NavMeshAgent GetAgent {  get { return _myAgent.GetNavAgent; } }

        [SerializeField] private float _attackRange;

        public override StateType OnStateUpdate()
        {
            if(_myAgent.target != null)
            {
                if (Vector3.Distance(_myAgent.transform.position, _myAgent.target.position) <= _attackRange && _myAgent.GetComponent<Attack>() != null)
                {
                    return StateType.Attack;
                }
                else if (Vector3.Distance(_myAgent.transform.position, _myAgent.target.position) <= _attackRange)
                {
                    return StateType.Melee;
                }
            }

            return GetStateType;
        }
    }
}
