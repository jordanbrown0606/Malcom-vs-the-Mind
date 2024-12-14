using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Attack : StateBase
    {
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private GameObject _fireballPrefab;
        [SerializeField] private float _attackFrequency;

        private float _timeSinceLastAttack;

        public override StateType GetStateType {  get { return StateType.Attack; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        private void Start()
        {
            _timeSinceLastAttack = _attackFrequency;
        }

        public override void OnStateEnter()
        {
            InitiateAttack();
        }

        public override StateType OnStateUpdate()
        {
            if (_myAgent.target == null && _myAgent.GetComponent<Idle>() != null)
            {
                return StateType.Idle;
            }
            else if(Vector3.Distance(_myAgent.transform.position, _myAgent.target.position) > _attackRange && _myAgent.GetComponent<Chase>() != null)
            {
                return StateType.Chase;
            }
            else
            {
                _timeSinceLastAttack -= Time.deltaTime;
                
                if(_timeSinceLastAttack <= 0)
                {
                    InitiateAttack();
                    _timeSinceLastAttack = _attackFrequency;
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
