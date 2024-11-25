using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Attack : StateBase
    {
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private GameObject _fireballPrefab;

        private float _timeSinceLastAttack = 1.5f;

        public override StateType GetStateType {  get { return StateType.Attack; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        public override void OnStateEnter()
        {
            InitiateAttack();
        }

        public override StateType OnStateUpdate()
        {
            if (_myAgent.target == null)
            {
                return StateType.Idle;
            }
            else
            {
                _timeSinceLastAttack -= Time.deltaTime;
                
                if(_timeSinceLastAttack <= 0)
                {
                    InitiateAttack();
                    _timeSinceLastAttack = 1.5f;
                }
            }

            return GetStateType;
        }

        private void InitiateAttack()
        {
            Vector3 attackPos = new Vector3(0f, 0.5f, 0f);
            GameObject curFireball = Instantiate(_fireballPrefab, transform.position + attackPos, transform.rotation);
            Vector3 force = _myAgent.target.transform.position - transform.position;

            curFireball.GetComponent<Rigidbody>().AddForce(force * _attackSpeed, ForceMode.Impulse);
        }
    }
}
