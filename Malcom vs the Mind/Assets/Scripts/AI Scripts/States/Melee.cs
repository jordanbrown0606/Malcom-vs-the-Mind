using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Melee : StateBase
    {
        [SerializeField] private float _attackRange;

        [SerializeField] private Animator _anim;

        [SerializeField] private GameObject _hitbox;
        public override StateType GetStateType { get { return StateType.Melee; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        public override void OnStateEnter()
        {
            _anim.SetBool("isAttacking", true);
        }

        public override StateType OnStateUpdate()
        {
            if (Vector3.Distance(_myAgent.target.position, _myAgent.transform.position) > _attackRange)
            {
                return StateType.Chase;
            }

            return GetStateType;
        }

        public override void OnStateExit()
        {
            _anim.SetBool("isAttacking", false);
        }

        public void ActivateHitbox()
        {
            _hitbox.SetActive(true);
        }

        public void DeactivateHitbox()
        {
            _hitbox.SetActive(false);
        }
    }
}
