using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{


    public class Patrol : StateBase
    {
        public override StateType GetStateType { get { return StateType.Patrol; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        public Transform[] _checkpoints;
        private int _curCheckpointDest = 0;

        public override void OnStateEnter()
        {
            _myAgent.GetNavAgent.SetDestination(_checkpoints[_curCheckpointDest].position);
        }

        public override StateType OnStateUpdate()
        {
            if (_myAgent.GetNavAgent.pathPending == false)
            {
                //If the distance left in the path is less than the arrive distance, change the current waypoint to the next waypoint.
                if (_myAgent.GetNavAgent.remainingDistance <= _myAgent.GetNavAgent.stoppingDistance)
                {
                    GoToNextWaypoint();
                }
            }

            if(_myAgent.target != null)
            {
                return StateType.Chase;
            }


            return GetStateType;
        }

        private void GoToNextWaypoint()
        {
            _curCheckpointDest++;

            // If the _curCheckpointDest is more than or the same value as the length, reset the destination to the first index.
            if (_curCheckpointDest >= _checkpoints.Length)
            {
                _curCheckpointDest = 0;
            }
            _myAgent.GetNavAgent.SetDestination(_checkpoints[_curCheckpointDest].position);
        }
    }
}
