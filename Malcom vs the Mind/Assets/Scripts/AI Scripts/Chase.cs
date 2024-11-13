using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Chase : StateBase
    {
        public override StateType GetStateType { get { return StateType.Chase; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        [SerializeField] private float _stopChasingRange;
        [SerializeField] private float _chasingSpeed;
        [SerializeField] private float _attackRange;

        public override StateType OnStateUpdate()
        {
            if (_myAgent.target == null)
            {
                return StateType.Patrol;
            }


            // If there is no path currently being calculated and the distance between the target and the agent is greater than the arrival range, recalculate the path to the target.
            if (_myAgent.GetNavAgent.pathPending == false && Vector3.Distance(_myAgent.GetNavAgent.destination, _myAgent.target.position) >= _myAgent.GetNavAgent.stoppingDistance)
            {
                _myAgent.GetNavAgent.SetDestination(_myAgent.target.position);
            }

            if (_myAgent.GetNavAgent.remainingDistance >= _stopChasingRange)
            {
                return StateType.Patrol;
            }

            if (Vector3.Distance(_myAgent.transform.position, _myAgent.target.position) <= _attackRange)
            {
                return StateType.Attack;
            }

            return GetStateType;
        }
    }
}
