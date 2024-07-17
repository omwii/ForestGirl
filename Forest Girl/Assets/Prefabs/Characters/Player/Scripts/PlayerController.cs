using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        //Serializable Variables
        [Header("Main settings")] //MAIN
        [SerializeField] private float _gravityForce = -9.81f;
        [SerializeField] private float _lookSensitivity = 15;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private bool _showDebugInfo = false;
        [SerializeField] private Image _fadein;
        [Header("Walk settings")] //WALK
        [SerializeField] private float _walkSpeed = 3;
        [SerializeField] private AudioSource _movementAudioSource;
        [SerializeField] private AudioClip _movementAudioClip;
        [SerializeField] private float _walkSoundPitch = 0.8f;
        [SerializeField] private float _walkSoundVolume = 1;
        [Header("Crouch settings")] //CROUCH
        [SerializeField] private float _crouchSpeed = 2;
        [SerializeField] private float _crouchTargetHeight = 0.7f;
        [SerializeField] private float _crouchSmoothingTime = 0.3f;
        [SerializeField] private AudioSource _crouchDownAudioSource;
        [SerializeField] private AudioSource _crouchUpAudioSource;
        [SerializeField] private AudioClip[] _crouchDownAudioClips;
        [SerializeField] private AudioClip[] _crouchUpAudioClips;
        [SerializeField] private float _crouchedSoundPitch = 1;
        [SerializeField] private float _crouchedSoundVolume = 1;
        [Header("Run settings")] //RUN
        [SerializeField] private float _runSpeed = 5;
        [SerializeField] private float _maxStaminaValue;
        [SerializeField] private float _staminaRegenSpeed;
        [SerializeField] private float _staminaWasteSpeed;
        [SerializeField] private float _runSoundPitch = 1;
        [SerializeField] private float _runSoundVolume = 1;
        [Header("Jump Settings")] //JUMP
        [SerializeField] private float _jumpForce = 6;
        [SerializeField] private float _airMovementSpeed = 3;
        [SerializeField] private AudioSource _jumpAudioSource;
        [SerializeField] private AudioSource _landAudioSource;
        [SerializeField] private AudioClip[] _jumpAudioClips;
        [SerializeField] private AudioClip[] _landAudioClips;
        [Header("Interaction Settings")] //INTERACTION
        [SerializeField] private float _interactionDistance = 4;
        [SerializeField] private Image _cursorImage;
        [SerializeField] private Sprite _defaultCursorSprite;
        [SerializeField] private Sprite _interactCursorSprite;
        [SerializeField] private Sprite _grabCursorSprite;
        [Header("Inventory Settings")] //INVENTORY
        [SerializeField] private Image[] _inventorySlotsIcons;
        [SerializeField] private Sprite _transparentIcon;

        //Public Fields
        public FSM_State CurrentState { get { return Fsm.CurrentState; } private set { } }
        public float VerticalVelocity { get; set; }
        public InventoryPerformer InventoryPerformer { get { return _inventoryPerformer; } private set { } }

        //Private Fields
        private InputActionsAsset _actions;
        private InputHandler _inputHandler;
        private MovementPerformer _movementPerformer;
        private LookPerformer _lookPerformer;
        private JumpPerformer _jumpPerformer;
        private GravityPerformer _gravityPerformer;
        private CrouchPerformer _crouchPerformer;
        private AudioPerformer _audioPerformer;
        private InteractionPerformer _interactionPerformer;
        private InventoryPerformer _inventoryPerformer;
        private AnimationPerformer _animationPerformer;
        private StaminaPerformer _staminaPerformer;
        private FSM Fsm;
        private CharacterController _characterController;
        private Animator _animator;
        private int _layerMask;

        #region MONO
        private void Awake()
        {
            GetComponents();
            SetPerformers();
            SetFSM();
            SetMisc();
        }

        private void Update()
        {
            Fsm.CurrentState.Update();
        }

        private void OnGUI()
        {
            if (!_showDebugInfo)
                return;

            GUI.TextArea(new Rect(10, 10, 250, 50), 
                $"Player State: {Fsm.CurrentState}\n" + //DebugContent
                $"Player horizontal velocity: {new Vector2(_characterController.velocity.x, _characterController.velocity.z)}\n" +
                $"Player Vertical velocity: {VerticalVelocity}" ); 
        }
        #endregion

        //Public Methods
        public void DummyIn(Transform targetLookTransform, Transform targetPosTrasform)
        {
            Fsm.SetState<FSM_StateDummy>();
            transform.DOMove(targetPosTrasform.position, 0.5f);
            transform.DODynamicLookAt(targetLookTransform.position, 0.5f, AxisConstraint.Y);
            _cameraTransform.DODynamicLookAt(targetLookTransform.position, 0.5f);
        }

        public void DummyOut(Transform unHideTransform)
        {
            Fsm.SetState<FSM_StateIdle>();
            transform.DOMove(unHideTransform.position, 0.5f);
        }

        public void Effects()
        {
            _cameraTransform.DOShakePosition(2f, 0.2f);
            _cameraTransform.DOShakeRotation(2f, 0.4f);
            _fadein.DOFade(1, 2f);
        }

        //Private Methods
        private void GetComponents()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _layerMask = 1 << 6;
        }

        private void SetPerformers()
        {
            _actions = new InputActionsAsset();
            _actions.Enable();
            _inputHandler = new InputHandler(_actions);

            _movementPerformer = new MovementPerformer(_characterController);

            _lookPerformer = new LookPerformer(transform, _cameraTransform, _lookSensitivity);

            _gravityPerformer = new GravityPerformer(this, _characterController, _gravityForce);

            _jumpPerformer = new JumpPerformer(this, _jumpForce);

            _crouchPerformer = new CrouchPerformer(_cameraTransform, _characterController, _layerMask,
                _crouchTargetHeight, _crouchSmoothingTime);

            _audioPerformer = new AudioPerformer(_movementAudioSource, _jumpAudioSource,
                _landAudioSource, _crouchDownAudioSource, _crouchUpAudioSource,
                _movementAudioClip, _jumpAudioClips, _landAudioClips,
                _crouchDownAudioClips, _crouchUpAudioClips, _walkSoundPitch,
                _walkSoundVolume, _crouchedSoundPitch, _crouchedSoundVolume,
                _runSoundPitch, _runSoundVolume);

            _interactionPerformer = new InteractionPerformer(_cameraTransform, _interactionDistance, _cursorImage,
                _defaultCursorSprite, _interactCursorSprite, _grabCursorSprite);

            _inventoryPerformer = new InventoryPerformer(_inventorySlotsIcons, _cameraTransform, _transparentIcon);

            _animationPerformer = new AnimationPerformer(_animator);

            _staminaPerformer = new StaminaPerformer(_maxStaminaValue, _staminaRegenSpeed, _staminaWasteSpeed);
        }

        private void SetFSM()
        {
            Fsm = new FSM();

            Fsm.AddState(new FSM_StateIdle(Fsm, _inputHandler, _lookPerformer, _movementPerformer,
                _gravityPerformer, _interactionPerformer, _inventoryPerformer,
                _animationPerformer, _staminaPerformer));

            Fsm.AddState(new FSM_StateWalk(Fsm, _inputHandler, _lookPerformer, _movementPerformer,
                _gravityPerformer, _audioPerformer, _interactionPerformer,
                _inventoryPerformer, _animationPerformer, _staminaPerformer,
                _walkSpeed));

            Fsm.AddState(new FSM_StateRun(Fsm, _inputHandler, _lookPerformer, _movementPerformer,
                _gravityPerformer, _audioPerformer, _interactionPerformer,
                _inventoryPerformer, _animationPerformer, _staminaPerformer,
                _runSpeed));

            Fsm.AddState(new FSM_StateJump(Fsm, _characterController, _inputHandler, _lookPerformer,
                _movementPerformer, _jumpPerformer, _gravityPerformer,
                _audioPerformer, _interactionPerformer, _inventoryPerformer,
                _animationPerformer, _staminaPerformer, _airMovementSpeed));

            Fsm.AddState(new FSM_StateCrouch(Fsm, _inputHandler, _lookPerformer, _movementPerformer,
                _gravityPerformer, _crouchPerformer, _audioPerformer,
                _interactionPerformer, _inventoryPerformer, _animationPerformer, 
                _staminaPerformer, _crouchSpeed));

            Fsm.AddState(new FSM_StateCrouchIdle(Fsm, _inputHandler, _lookPerformer, _movementPerformer, 
                _gravityPerformer, _crouchPerformer, _interactionPerformer,
                _inventoryPerformer, _animationPerformer, _staminaPerformer));

            Fsm.AddState(new FSM_StateDummy(Fsm, _inputHandler, _interactionPerformer));

            Fsm.SetState<FSM_StateIdle>();
        }

        private void SetMisc()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}