using Player;
using UnityEngine;
using UnityEngine.AI;
namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Patroling settings")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _playerAttackTargetTransform;
        [SerializeField] private Transform _playerAttackLookTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private AudioSource _attackAudioSource;
        [SerializeField] private Item[] _soundItems;
        [SerializeField] private float _idleTime;
        [SerializeField] private float _attackTransition;
        [SerializeField] private bool _showDebugInfo = false;

        public FSM_State CurrentPlayerState { get { return _playerController.CurrentState; } }

        private FSM _fsm;
        private MovementPerformer _movementPerformer;
        private ViewPerformer _viewPerformer;
        private PatrolPerformer _patrolPerformer;
        private AnimationPerformer _animationPerformer;
        private ListenPerformer _listenPerformer;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        #region MONO
        private void Awake()
        {
            GetComponents();
            SetPerformers();
            SetFSM();
        }

        private void Update()
        {
            _fsm.CurrentState.Update();
        }

        private void OnGUI()
        {
            if (!_showDebugInfo)
                return;

            GUI.TextArea(new Rect(270, 10, 250, 50),
                $"Enemy State: {_fsm.CurrentState}");
        }
        #endregion

        //Private Methods
        private void SetPerformers()
        {
            _movementPerformer = new MovementPerformer(_navMeshAgent);

            _patrolPerformer = new PatrolPerformer(_patrolPoints);

            _animationPerformer = new AnimationPerformer(_animator, _attackTransition);

            _listenPerformer = new ListenPerformer(_soundItems);
        }

        private void GetComponents()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _viewPerformer = GetComponent<ViewPerformer>();
            _animator = GetComponent<Animator>();
        }

        private void SetFSM()
        {
            _fsm = new FSM();

            _fsm.AddState(new FSM_StateIdle(_fsm, this, _viewPerformer, _animationPerformer, _listenPerformer, _idleTime));

            _fsm.AddState(new FSM_StatePatrol(_fsm, this, _movementPerformer, _viewPerformer,
                _patrolPerformer, _animationPerformer, _listenPerformer, _navMeshAgent));

            _fsm.AddState(new FSM_StateChase(_fsm, this, _movementPerformer, _animationPerformer, _viewPerformer,
                _playerTransform, _navMeshAgent));

            _fsm.AddState(new FSM_StateAttack(_fsm, _animationPerformer, _playerController, _playerAttackLookTransform,
                _playerTransform, _playerAttackTargetTransform, _attackAudioSource));

            _fsm.AddState(new FSM_StateListen(_fsm, _listenPerformer, _animationPerformer, this, _viewPerformer, _navMeshAgent));

            _fsm.AddState(new FSM_State(_fsm));

            _fsm.SetState<FSM_State>();
        }

        //Public Methods
        public void OnBasementUnlock()
        {
            _fsm.SetState<FSM_StateIdle>();
        }
    }
}