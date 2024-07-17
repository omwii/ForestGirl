using UnityEngine;

namespace Player
{
    public class FSM_StateIdle : FSM_State
    {
        private InputHandler _inputHandler;
        private LookPerformer _lookPerformer;
        private MovementPerformer _movementPerformer;
        private GravityPerformer _gravityPerformer;
        private InteractionPerformer _interactionPerformer;
        private InventoryPerformer _inventoryPerformer;
        private AnimationPerformer _animationPerformer;
        private StaminaPerformer _staminaPerformer;

        #region FSM
        public FSM_StateIdle(FSM fsm,
            InputHandler inputHandler,
            LookPerformer lookPerformer,
            MovementPerformer movementPerformer,
            GravityPerformer gravityPerformer,
            InteractionPerformer interactionPerformer,
            InventoryPerformer inventoryPerformer,
            AnimationPerformer animationPerformer,
            StaminaPerformer staminaPerformer) : base(fsm)
        {
            _inputHandler = inputHandler;
            _lookPerformer = lookPerformer;
            _movementPerformer = movementPerformer;
            _gravityPerformer = gravityPerformer;
            _interactionPerformer = interactionPerformer;
            _inventoryPerformer = inventoryPerformer;
            _animationPerformer = animationPerformer;
            _staminaPerformer = staminaPerformer;
        }

        public override void Enter()
        {
            // Unity Events
            _inputHandler.OnJump.AddListener(SetJumpState);
            _inputHandler.OnCrouch.AddListener(SetCrouchState);
            _inputHandler.OnInteract.AddListener(Interact);
            _inputHandler.OnThrow.AddListener(ThrowItem);
            _inputHandler.OnFirst.AddListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.AddListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.AddListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.AddListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.AddListener(delegate { ChangeSlot(4); });

            _animationPerformer.SetMovingState(false);
        }

        public override void Update()
        {
            _lookPerformer.Look(_inputHandler.GetLookDir());
            _movementPerformer.Move(_inputHandler.GetMoveDir(), 0);
            _gravityPerformer.ApplyGravity();
            _interactionPerformer.CursorSpriteChange();
            _animationPerformer.UpdateAnimationParameters(_movementPerformer.CurrentDesiredInputDirection.x, _movementPerformer.CurrentDesiredInputDirection.z, _inputHandler.IsCrouching);
            _staminaPerformer.StaminaLogic();
            TrySetWalkState();
        }

        public override void Exit()
        {
            // Unity Events
            _inputHandler.OnJump.RemoveListener(SetJumpState);
            _inputHandler.OnCrouch.RemoveListener(SetCrouchState);
            _inputHandler.OnInteract.RemoveListener(Interact);
            _inputHandler.OnThrow.RemoveListener(ThrowItem);
            _inputHandler.OnFirst.RemoveListener(delegate { ChangeSlot(0); });
            _inputHandler.OnSecond.RemoveListener(delegate { ChangeSlot(1); });
            _inputHandler.OnThird.RemoveListener(delegate { ChangeSlot(2); });
            _inputHandler.OnFourth.RemoveListener(delegate { ChangeSlot(3); });
            _inputHandler.OnFifth.RemoveListener(delegate { ChangeSlot(4); });

            _animationPerformer.SetMovingState(true);
        }
        #endregion

        //Private Methods
        private void TrySetWalkState()
        {
            if (_inputHandler.GetMoveDir() != Vector2.zero)
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