using UnityEngine;

namespace Player
{
    public class FSM_StateRun : FSM_State
    {
        private InputHandler _inputHandler;
        private LookPerformer _lookPerformer;
        private MovementPerformer _movementPerformer;
        private GravityPerformer _gravityPerformer;
        private AudioPerformer _audioPerformer;
        private InteractionPerformer _interactionPerformer;
        private InventoryPerformer _inventoryPerformer;
        private AnimationPerformer _animationPerformer;
        private StaminaPerformer _staminaPerformer;
        private float _runSpeed;

        #region FSM
        public FSM_StateRun(FSM fsm,
            InputHandler inputHandler,
            LookPerformer lookPerformer,
            MovementPerformer movementPerformer,
            GravityPerformer gravityPerformer,
            AudioPerformer audioPerformer,
            InteractionPerformer interactionPerformer,
            InventoryPerformer inventoryPerformer,
            AnimationPerformer animationPerformer,
            StaminaPerformer staminaPerformer,
            float runSpeed) : base(fsm)
        {
            _inputHandler = inputHandler;
            _lookPerformer = lookPerformer;
            _movementPerformer = movementPerformer;
            _gravityPerformer = gravityPerformer;
            _audioPerformer = audioPerformer;
            _interactionPerformer = interactionPerformer;
            _inventoryPerformer = inventoryPerformer;
            _animationPerformer = animationPerformer;
            _staminaPerformer = staminaPerformer;
            _runSpeed = runSpeed;
        }

        public override void Enter()
        {
            //Unity Events
            _inputHandler.OnJump.AddListener(SetJumpState);
            _inputHandler.OnCrouch.AddListener(SetCrouchState);
            _inputHandler.OnInteract.AddListener(Interact);
            _inputHandler.OnThrow.AddListener(ThrowItem);
            _inputHandler.OnFirst.AddListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.AddListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.AddListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.AddListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.AddListener(delegate { ChangeSlot(4); });
            //Audio
            _audioPerformer.StartMovementSound(AudioPerformer.MovementType.Run);
            //Misc
            _staminaPerformer.IsRunning = true;
        }

        public override void Update()
        {
            TrySetIdleState();
            TrySetWalkState();
            _lookPerformer.Look(_inputHandler.GetLookDir());
            _movementPerformer.Move(_inputHandler.GetMoveDir(), _runSpeed);
            _gravityPerformer.ApplyGravity();
            _interactionPerformer.CursorSpriteChange();
            _animationPerformer.UpdateAnimationParameters(_movementPerformer.CurrentDesiredInputDirection.x, _movementPerformer.CurrentDesiredInputDirection.z, _inputHandler.IsCrouching);
            _staminaPerformer.StaminaLogic();
        }

        public override void Exit()
        {
            //Unity Events
            _inputHandler.OnJump.RemoveListener(SetJumpState);
            _inputHandler.OnCrouch.RemoveListener(SetCrouchState);
            _inputHandler.OnInteract.RemoveListener(Interact);
            _inputHandler.OnThrow.RemoveListener(ThrowItem);
            _inputHandler.OnFirst.RemoveListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.RemoveListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.RemoveListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.RemoveListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.RemoveListener(delegate { ChangeSlot(4); });
            //Audio
            _audioPerformer.PauseMovementSound();
            //Misc
            _staminaPerformer.IsRunning = false;
        }
        #endregion

        //Private Methods
        private void TrySetIdleState()
        {
            if (_inputHandler.GetMoveDir() == Vector2.zero && _movementPerformer.Controller.velocity == new Vector3(0, _movementPerformer.Controller.velocity.y, 0))
                Fsm.SetState<FSM_StateIdle>();
            if (_staminaPerformer.StaminaValue <= 0.1f)
            {
                Fsm.SetState<FSM_StateWalk>();
            }
        }

        private void SetJumpState()
        {
            Fsm.SetState<FSM_StateJump>();
        }

        private void SetCrouchState()
        {
            Fsm.SetState<FSM_StateCrouch>();
        }

        private void TrySetWalkState()
        {
            if (!_inputHandler.IsRunning)
            {
                Fsm.SetState<FSM_StateWalk>();
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