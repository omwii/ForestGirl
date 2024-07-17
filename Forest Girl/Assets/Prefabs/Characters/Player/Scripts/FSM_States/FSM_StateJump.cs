using UnityEngine;

namespace Player
{
    public class FSM_StateJump : FSM_State
    {
        private CharacterController _characterController;
        private InputHandler _inputHandler;
        private LookPerformer _lookPerformer;
        private MovementPerformer _movementPerformer;
        private JumpPerformer _jumpPerformer;
        private GravityPerformer _gravityPerformer;
        private AudioPerformer _audioPerformer;
        private InteractionPerformer _interactionPerformer;
        private InventoryPerformer _inventoryPerformer;
        private AnimationPerformer _animationPerformer;
        private StaminaPerformer _staminaPerformer;
        private float _airMovementSpeed;

        #region FSM
        public FSM_StateJump(FSM fsm,
            CharacterController characterController,
            InputHandler inputHandler,
            LookPerformer lookPerformer,
            MovementPerformer movementPerformer,
            JumpPerformer jumpPerformer,
            GravityPerformer gravityPerformer,
            AudioPerformer audioPerformer,
            InteractionPerformer interactionPerformer,
            InventoryPerformer inventoryPerformer,
            AnimationPerformer animationPerformer,
            StaminaPerformer staminaPerformer,
            float airMovementSpeed) : base(fsm)
        {
            _characterController = characterController;
            _inputHandler = inputHandler;
            _lookPerformer = lookPerformer;
            _movementPerformer = movementPerformer;
            _jumpPerformer = jumpPerformer;
            _gravityPerformer = gravityPerformer;
            _audioPerformer = audioPerformer;
            _interactionPerformer = interactionPerformer;
            _inventoryPerformer = inventoryPerformer;
            _animationPerformer = animationPerformer;
            _staminaPerformer = staminaPerformer;
            _airMovementSpeed = airMovementSpeed;
        }

        public override void Enter()
        {
            _inputHandler.OnCrouch.AddListener(SetCrouchState);
            _inputHandler.OnInteract.AddListener(Interact);
            _inputHandler.OnThrow.AddListener(ThrowItem);
            _inputHandler.OnFirst.AddListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.AddListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.AddListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.AddListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.AddListener(delegate { ChangeSlot(4); });
            _audioPerformer.JumpSound();
            _jumpPerformer.Jump();
            _animationPerformer.Jump();
        }

        public override void Update()
        {
            _lookPerformer.Look(_inputHandler.GetLookDir());
            _movementPerformer.Move(_inputHandler.GetMoveDir(), _airMovementSpeed);
            _gravityPerformer.ApplyGravity();
            _interactionPerformer.CursorSpriteChange();
            _animationPerformer.UpdateAnimationParameters(_movementPerformer.CurrentDesiredInputDirection.x, _movementPerformer.CurrentDesiredInputDirection.z, _inputHandler.IsCrouching);
            _staminaPerformer.StaminaLogic();
            CheckIfLanded();
        }

        public override void Exit()
        {
            _inputHandler.OnCrouch.RemoveListener(SetCrouchState);
            _inputHandler.OnInteract.RemoveListener(Interact);
            _inputHandler.OnThrow.RemoveListener(ThrowItem);
            _inputHandler.OnFirst.RemoveListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.RemoveListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.RemoveListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.RemoveListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.RemoveListener(delegate { ChangeSlot(4); });
            _audioPerformer.LandSound();
        }
        #endregion

        //Private methods
        private void CheckIfLanded()
        {
            if (_characterController.isGrounded)
            {
                Fsm.SetState<FSM_StateWalk>();
            }
        }

        private void SetCrouchState()
        {
            Fsm.SetState<FSM_StateCrouch>();
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