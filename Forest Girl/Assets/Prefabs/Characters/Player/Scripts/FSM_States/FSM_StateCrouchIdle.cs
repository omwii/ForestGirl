using UnityEngine;

namespace Player
{
    public class FSM_StateCrouchIdle : FSM_State
    {
        private InputHandler _inputHandler;
        private LookPerformer _lookPerformer;
        private MovementPerformer _movementPerformer;
        private GravityPerformer _gravityPerformer;
        private CrouchPerformer _crouchPerformer;
        private InteractionPerformer _interactionPerformer;
        private InventoryPerformer _inventoryPerformer;
        private AnimationPerformer _animationPerformer;
        private StaminaPerformer _staminaPerformer;

        #region FSM
        public FSM_StateCrouchIdle(FSM fsm,
            InputHandler inputHandler,
            LookPerformer lookPerformer,
            MovementPerformer movementPerformer,
            GravityPerformer gravityPerformer,
            CrouchPerformer crouchPerformer,
            InteractionPerformer interactionPerformer,
            InventoryPerformer inventoryPerformer,
            AnimationPerformer animationPerformer,
            StaminaPerformer staminaPerformer) : base(fsm)
        {
            _inputHandler = inputHandler;
            _lookPerformer = lookPerformer;
            _movementPerformer = movementPerformer;
            _gravityPerformer = gravityPerformer;
            _crouchPerformer = crouchPerformer;
            _interactionPerformer = interactionPerformer;
            _inventoryPerformer = inventoryPerformer;
            _animationPerformer = animationPerformer;
            _staminaPerformer = staminaPerformer;
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

            _animationPerformer.SetMovingState(false);
        }

        public override void Update()
        {
            _gravityPerformer.ApplyGravity();
            _lookPerformer.Look(_inputHandler.GetLookDir());
            _interactionPerformer.CursorSpriteChange();
            _movementPerformer.Move(_inputHandler.GetMoveDir(), 0);
            _animationPerformer.UpdateAnimationParameters(_movementPerformer.CurrentDesiredInputDirection.x, _movementPerformer.CurrentDesiredInputDirection.z, _inputHandler.IsCrouching);
            _staminaPerformer.StaminaLogic();
            TrySetIdleState();
            TrySetCrouchState();
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

            _animationPerformer.SetMovingState(true);
        }
        #endregion

        //Private Methods
        private void TrySetIdleState()
        {
            if (!_inputHandler.IsCrouching && _crouchPerformer.CheckIsFreeAbove())
            {
                Fsm.SetState<FSM_StateIdle>();
            }
        }

        private void TrySetCrouchState()
        {
            if (_inputHandler.GetMoveDir() != Vector2.zero)
            {
                Fsm.SetState<FSM_StateCrouch>();
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