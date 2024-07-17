using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class FSM_StateAttack : FSM_State
    {
        private AnimationPerformer _animationPerformer;
        private PlayerController _playerController;
        private Transform _enemyTransform;
        private Transform _playerTransform;
        private Transform _attackPlayerTargetTransform;
        private AudioSource _audioSource;

        #region FSM
        public FSM_StateAttack(FSM fsm,
            AnimationPerformer animationPerformer,
            PlayerController playerController,
            Transform enemyTransform,
            Transform playerTransform,
            Transform attackPlayerTargetTransform,
            AudioSource audioSource) : base(fsm)
        {
            _animationPerformer = animationPerformer;
            _playerController = playerController;
            _enemyTransform = enemyTransform;
            _playerTransform = playerTransform;
            _attackPlayerTargetTransform = attackPlayerTargetTransform;
            _audioSource = audioSource;
        }

        public override void Enter()
        {
            _enemyTransform.DODynamicLookAt(_playerTransform.position, 0.3f, AxisConstraint.Y);
            _animationPerformer.SwitchMoveAnimState(false);
            _animationPerformer.AttackAnimation();
            _playerController.DummyIn(_enemyTransform, _attackPlayerTargetTransform);
            _playerController.Effects();
            _audioSource.Play();
        }
        #endregion
    }
}