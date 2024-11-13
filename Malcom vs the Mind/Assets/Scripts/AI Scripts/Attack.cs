using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Attack : StateBase
    {
        [SerializeField] private float _attackRange;
        [SerializeField] private GameObject _hitBox;
        [SerializeField] private Animator _anim;

        public override StateType GetStateType {  get { return StateType.Attack; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        private void Start()
        {
            _anim = _myAgent.GetComponent<Animator>();
        }

        public override void OnStateEnter()
        {
            _anim.SetBool("isWalking", false);
            _anim.SetBool("IsAttacking", true);
        }

        public override StateType OnStateUpdate()
        {
            if (Vector3.Distance(_myAgent.target.position, _myAgent.transform.position) > _attackRange)
            {
                _anim.SetBool("IsAttacking", false);
                _anim.SetBool("isWalking", true);
                return StateType.Chase;
            }

            return GetStateType;
        }

        public void ActivateHitBox()
        {
            _hitBox.SetActive(true);
        }

        public void DeactivateHitBox()
        {
            _hitBox.SetActive(false);
        }
    }
}
