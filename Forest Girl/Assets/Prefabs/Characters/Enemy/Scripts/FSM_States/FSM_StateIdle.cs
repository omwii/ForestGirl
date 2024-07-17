using Player;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class FSM_StateIdle : FSM_State
    {
        private EnemyController _enemyController;
        private ViewPerformer _viewPerformer;
        private AnimationPerformer _animationPerformer;
        private ListenPerformer _listenPerformer;
        private float _idleTime;
        private float _currentIdleTime;
        private bool _canListen = false;

        #region FSM
        public FSM_StateIdle(FSM fsm,
            EnemyController enemyController,
            ViewPerformer viewPerformer,
            AnimationPerformer animationPerformer,
            ListenPerformer listenPerformer,
            float idleTime) : base(fsm)
        {
            _enemyController = enemyController;
            _viewPerformer = viewPerformer;
            _animationPerformer = animationPerformer;
            _listenPerformer = listenPerformer;
            _idleTime = idleTime;

            SetListenTrue();
        }

        public override void Enter()
        {
            _animationPerformer.SwitchMoveAnimState(false);
            ResetIdleTime();
            _listenPerformer.OnCollisionEvent.AddListener(SetListenState);
        }

        public override void Update()
        {
            TryChasePlayer();
            UpdateIdleTime();
        }

        public override void Exit()
        {
            _listenPerformer.OnCollisionEvent.RemoveListener(SetListenState);
        }
        #endregion

        //Private Methods

        #region Idle
        private void ResetIdleTime()
        {
            _currentIdleTime = 0;
        }
        
        private void UpdateIdleTime()
        {
            _currentIdleTime += Time.deltaTime;
            if (_currentIdleTime >= _idleTime)
            {
                Fsm.SetState<FSM_StatePatrol>();
            }
        }
        #endregion

        private async void SetListenTrue()
        {
            await Task.Delay(3000);
            _canListen = true;
        }

        private void TryChasePlayer()
        {
            _viewPerformer.FieldOfViewCheck();
            if (_viewPerformer.CanSeePlayer && _enemyController.CurrentPlayerState.GetType() != typeof(FSM_StateDummy))
            {
                Fsm.SetState<FSM_StateChase>();
            }
        }

        private void SetListenState()
        {
            if (!_canListen)
                return;
            Fsm.SetState<FSM_StateListen>();
        }
    }
}