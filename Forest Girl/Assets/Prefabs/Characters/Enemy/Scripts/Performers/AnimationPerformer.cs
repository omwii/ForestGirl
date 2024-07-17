using UnityEngine;

namespace Enemy
{
    public class AnimationPerformer
    {
        private Animator _animator;
        private float _attackTransition;

        //Public methods
        public void SwitchMoveAnimState(bool isMoving)
        {
            _animator.SetBool("Moving", isMoving);
        }

        public void AttackAnimation()
        {
            _animator.CrossFade("Attack", _attackTransition);
        }

        //Constructor
        public AnimationPerformer(Animator animator,
            float attackTransition)
        {
            _animator = animator;
            _attackTransition = attackTransition;
        }
    }
}