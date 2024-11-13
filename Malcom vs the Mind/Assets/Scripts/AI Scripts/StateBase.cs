using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public abstract class StateBase : MonoBehaviour
    {
        [SerializeField] protected Agent _myAgent;

        public abstract StateType GetStateType { get; }

        public abstract NavMeshAgent GetAgent {  get; }

        public void InitState(Agent agent)
        {
            _myAgent = agent;
        }

        public virtual void OnStateEnter() { }
        public abstract StateType OnStateUpdate();
        public virtual void OnStateExit() { }
    }

    public enum StateType
    {
        Patrol,
        Chase,
        Attack,
        Death
    }
}