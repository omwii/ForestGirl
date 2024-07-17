using UnityEngine;
namespace Player
{
    public class FSM_StateDummy : FSM_State
    {
        private InputHandler _inputHandler;
        private InteractionPerformer _interactionPerformer;

        #region FSM
        public FSM_StateDummy(FSM fsm,
            InputHandler inputHandler,
            InteractionPerformer interactionPerformer) : base(fsm)
        {
            _inputHandler = inputHandler;
            _interactionPerformer = interactionPerformer;
        }

        public override void Enter()
        {
            _inputHandler.OnInteract.AddListener(Interact);
        }

        public override void Update()
        {
            _interactionPerformer.CursorSpriteChange();
        }

        public override void Exit()
        {
            _inputHandler.OnInteract.RemoveListener(Interact);
        }
        #endregion

        //Private methods
        private void Interact()
        {
            _interactionPerformer.Interact();
        }
    }
}