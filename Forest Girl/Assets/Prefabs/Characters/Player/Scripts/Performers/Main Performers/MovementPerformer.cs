using UnityEngine;

namespace Player
{
    public class MovementPerformer
    {
        public CharacterController Controller { get; private set; }
        public Vector3 CurrentDesiredInputDirection { get; private set; }

        public void Move(Vector2 movementDirection, float _movementSpeed)
        {
            Vector3 desiredInputDirection = new Vector3(
                movementDirection.x,
                0,
                movementDirection.y);

            CurrentDesiredInputDirection = Vector3.Lerp(CurrentDesiredInputDirection, desiredInputDirection * _movementSpeed, Time.deltaTime * 7);

            Vector3 moveVector = Controller.transform.TransformDirection(CurrentDesiredInputDirection);
            Vector3 desiredMovementDirection = moveVector * Time.deltaTime;

            Controller.Move(desiredMovementDirection);
        }

        //Constructor
        public MovementPerformer(CharacterController controller)
        {
            Controller = controller;
        }
    }
}
