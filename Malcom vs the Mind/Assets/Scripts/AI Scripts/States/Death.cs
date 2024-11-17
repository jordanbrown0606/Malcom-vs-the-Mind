using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BetterFSM
{
    public class Death : StateBase
    {
        [SerializeField] private Animator _anim;

        public AudioClip deathSound;
        public AudioSource audioSource;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public override StateType GetStateType { get { return StateType.Death; } }

        public override NavMeshAgent GetAgent { get { return _myAgent.GetNavAgent; } }

        public override void OnStateEnter()
        {
            _anim.SetBool("isWalking", false);
            _anim.SetBool("IsAttacking", false);
            _anim.SetBool("isDead", true);
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 8f);
        }

        public override StateType OnStateUpdate()
        {
            StartCoroutine(DeleteBody());
            return GetStateType;
        }

        public IEnumerator DeleteBody()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(this.gameObject);
        }

    }
}
