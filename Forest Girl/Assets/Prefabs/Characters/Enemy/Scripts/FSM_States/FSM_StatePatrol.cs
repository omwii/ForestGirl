using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FSM_StatePatrol : FSM_State
    {
        private EnemyController _enemyController;
        private MovementPerformer _movementPerformer;
        private ViewPerformer _viewPerformer;
        private PatrolPerformer _patrolPerformer;
        private AnimationPerformer _animationPerformer;
        private ListenPerformer _listenPerformer;
        private NavMeshAgent _meshAgent;
        private bool _canListen = false;

        #region FSM
        public FSM_StatePatrol(FSM fsm,
            EnemyController enemyController,
            MovementPerformer movementPerformer,
            ViewPerformer viewPerformer,
            PatrolPerformer patrolPerformer,
            AnimationPerformer animationPerformer,
            ListenPerformer listenPerformer,
            NavMeshAgent meshAgent) : base(fsm)
        {
            _enemyController = enemyController;
            _movementPerformer = movementPerformer;
            _viewPerformer = viewPerformer;
            _patrolPerformer = patrolPerformer;
            _animationPerformer = animationPerformer;
            _listenPerformer = listenPerformer;
            _meshAgent = meshAgent;

            SetListenTrue();
        }

        public override void Enter()
        {
            _movementPerformer.SetTargetMovementPoint(_patrolPerformer.ChooseRandomPatrolPoint());
            _listenPerformer.OnCollisionEvent.AddListener(SetListenState);
        }

        public override void Update()
        {
            TryChasePlayer();
            CheckIfPatrolPointReached();
        }

        public override void Exit()
        {
            _listenPerformer.OnCollisionEvent.RemoveListener(SetListenState);
        }
        #endregion

        //Private Methods
        private void TryChasePlayer()
        {
            _viewPerformer.FieldOfViewCheck();
            if (_viewPerformer.CanSeePlayer && _enemyController.CurrentPlayerState.GetType() != typeof(FSM_StateDummy))
            {
                Fsm.SetState<FSM_StateChase>();
            }
        }

        private void CheckIfPatrolPointReached()
        {
            if (!_meshAgent.pathPending)
            {
                if (_meshAgent.remainingDistance <= _meshAgent.stoppingDistance)
                {
                    if (!_meshAgent.hasPath || _meshAgent.velocity.sqrMagnitude == 0f)
                    {
                        Fsm.SetState<FSM_StateIdle>();
                    }
                    else
                    {
                        _animationPerformer.SwitchMoveAnimState(true);
                    }
                }
                else
                {
                    _animationPerformer.SwitchMoveAnimState(true);
                }
            }
            else
            {
                _animationPerformer.SwitchMoveAnimState(true);
            }
        }

        private void SetListenState()
        {
            if (!_canListen)
                return;
            Fsm.SetState<FSM_StateListen>();
        }

        private async void SetListenTrue()
        {
            await Task.Delay(3000);
            _canListen = true;
        }
    }
}