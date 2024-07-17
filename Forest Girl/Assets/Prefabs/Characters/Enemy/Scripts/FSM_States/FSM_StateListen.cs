using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FSM_StateListen : FSM_State
    {
        private ViewPerformer _viewPerformer;
        private EnemyController _enemyController;
        private ListenPerformer _listenPerformer;
        private AnimationPerformer _animationPerformer;
        private NavMeshAgent _meshAgent;

        #region FSM
        public FSM_StateListen(FSM fsm, 
            ListenPerformer listenPerformer,
            AnimationPerformer animationPerformer,
            EnemyController enemyController,
            ViewPerformer viewPerformer,
            NavMeshAgent meshAgent) : base(fsm)
        {
            _listenPerformer = listenPerformer;
            _animationPerformer = animationPerformer;
            _enemyController = enemyController;
            _viewPerformer = viewPerformer;
            _meshAgent = meshAgent;
        }
        public override void Enter()
        {
            _meshAgent.SetDestination(_listenPerformer.CurrentItemPosition);
        }

        public override void Update()
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
            _viewPerformer.FieldOfViewCheck();
            if (_viewPerformer.CanSeePlayer && _enemyController.CurrentPlayerState.GetType() != typeof(FSM_StateDummy))
            {
                Fsm.SetState<FSM_StateChase>();
            }
        }
        #endregion
    }
}