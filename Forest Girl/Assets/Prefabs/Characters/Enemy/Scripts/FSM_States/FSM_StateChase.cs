using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FSM_StateChase : FSM_State
    {
        private EnemyController _enemyController;
        private MovementPerformer _movementPerformer;
        private AnimationPerformer _animationPerformer;
        private ViewPerformer _viewPerformer;
        private Transform _playerTransform;
        private NavMeshAgent _meshAgent;

        #region FSM
        public FSM_StateChase(FSM fsm,
            EnemyController enemyController,
            MovementPerformer movementPerformer,
            AnimationPerformer animationPerformer,
            ViewPerformer viewPerformer,
            Transform playerTransform,
            NavMeshAgent meshAgent) : base(fsm)
        {
            _enemyController = enemyController;
            _movementPerformer = movementPerformer;
            _animationPerformer = animationPerformer;
            _viewPerformer = viewPerformer;
            _playerTransform = playerTransform;
            _meshAgent = meshAgent;
        }

        public override void Enter()
        {
            SetPlayerMovementPoint();
            _animationPerformer.SwitchMoveAnimState(true);
        }

        public override void Update()
        {
            CheckIfPlayerHide();
            TryAttack();
            TryExitChase();
        }
        #endregion

        //Private methods
        private void SetPlayerMovementPoint()
        {
            _movementPerformer.SetTargetMovementPoint(_playerTransform.position);
        }

        private void TryAttack()
        {
            if (_meshAgent.remainingDistance <= _meshAgent.stoppingDistance + 0.3f &&
                _viewPerformer.CanSeePlayer)
            {
                _meshAgent.isStopped = true;
                Fsm.SetState<FSM_StateAttack>();
            }
            else if (_meshAgent.remainingDistance > _meshAgent.stoppingDistance + 0.3f)
            {
                SetPlayerMovementPoint();
            }
        }

        private void TryExitChase()
        {
            if (_meshAgent.remainingDistance > _meshAgent.stoppingDistance + 0.3f &&
                !_viewPerformer.CanSeePlayer)
            {
                Fsm.SetState<FSM_StateIdle>();
            }
        }

        private void CheckIfPlayerHide()
        {
            if (_enemyController.CurrentPlayerState.GetType() == typeof(FSM_StateDummy))
            {
                Fsm.SetState<FSM_StateIdle>();
            }
        }
    }
}