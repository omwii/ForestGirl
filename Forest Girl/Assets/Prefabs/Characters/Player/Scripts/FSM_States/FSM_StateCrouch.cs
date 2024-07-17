using UnityEngine;

namespace Player
{
    public class FSM_StateCrouch : FSM_State
    {
        private InputHandler _inputHandler;
        private LookPerformer _lookPerformer;
        private MovementPerformer _movementPerformer;
        private GravityPerformer _gravityPerformer;
        private CrouchPerformer _crouchPerformer;
        private AudioPerformer _audioPerformer;
        private InteractionPerformer _interactionPerformer;
        private InventoryPerformer _inventoryPerformer;
        private AnimationPerformer _animationPerformer;
        private StaminaPerformer _staminaPerformer;
        private float _crouchSpeed;

        #region FSM
        public FSM_StateCrouch(FSM fsm,
            InputHandler inputHandler,
            LookPerformer lookPerformer,
            MovementPerformer movementPerformer,
            GravityPerformer gravityPerformer,
            CrouchPerformer crouchPerformer,
            AudioPerformer audioPerformer,
            InteractionPerformer interactionPerformer,
            InventoryPerformer inventoryPerformer,
            AnimationPerformer animationPerformer,
            StaminaPerformer staminaPerformer,
            float crouchSpeed) : base(fsm)
        {
            _inputHandler = inputHandler;
            _lookPerformer = lookPerformer;
            _movementPerformer = movementPerformer;
            _gravityPerformer = gravityPerformer;
            _crouchPerformer = crouchPerformer;
            _audioPerformer = audioPerformer;
            _interactionPerformer = interactionPerformer;
            _inventoryPerformer = inventoryPerformer;
            _animationPerformer = animationPerformer;
            _staminaPerformer = staminaPerformer;
            _crouchSpeed = crouchSpeed;
        }

        public override void Enter()
        {
            _inputHandler.OnInteract.AddListener(Interact);
            _inputHandler.OnThrow.AddListener(ThrowItem);
            _inputHandler.OnFirst.AddListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.AddListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.AddListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.AddListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.AddListener(delegate { ChangeSlot(4); });
            _crouchPerformer.Crouch();
            _audioPerformer.CrouchDownSound();
            _audioPerformer.StartMovementSound(AudioPerformer.MovementType.Crouch);
        }
        public override void Update()
        {
            _gravityPerformer.ApplyGravity();
            _lookPerformer.Look(_inputHandler.GetLookDir());
            _movementPerformer.Move(_inputHandler.GetMoveDir(), _crouchSpeed);
            _interactionPerformer.CursorSpriteChange();
            _animationPerformer.UpdateAnimationParameters(_movementPerformer.CurrentDesiredInputDirection.x, _movementPerformer.CurrentDesiredInputDirection.z, _inputHandler.IsCrouching);
            _staminaPerformer.StaminaLogic();
            TrySetWalkingState();
            TrySetCrouchIdleState();
        }
        public override void Exit()
        {
            _inputHandler.OnInteract.RemoveListener(Interact);
            _inputHandler.OnThrow.RemoveListener(ThrowItem);
            _inputHandler.OnFirst.RemoveListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.RemoveListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.RemoveListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.RemoveListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.RemoveListener(delegate { ChangeSlot(4); });
            _crouchPerformer.UnCrouch();
            _audioPerformer.CrouchUpSound();
            _audioPerformer.PauseMovementSound();
        }
        #endregion

        //Private Methods
        private void TrySetWalkingState()
        {
            if (!_inputHandler.IsCrouching && _crouchPerformer.CheckIsFreeAbove())
            {
                Fsm.SetState<FSM_StateWalk>();
                return;
            }
        }

        private void TrySetCrouchIdleState()
        {
            if (_inputHandler.IsCrouching && _inputHandler.GetMoveDir() == Vector2.zero)
            {
                Fsm.SetState<FSM_StateCrouchIdle>();
            }
        }

        private void Interact()
        {
            _interactionPerformer.Interact();
        }

        private void ThrowItem()
        {
            _inventoryPerformer.ThrowItem();
        }

        private void ChangeSlot(int targetSlot)
        {
            _inventoryPerformer.ChangeCurrentSlot(targetSlot);
        }
    }
}