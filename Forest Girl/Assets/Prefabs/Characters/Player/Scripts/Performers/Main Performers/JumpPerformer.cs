namespace Player
{
    public class JumpPerformer
    {
        private PlayerController _playerController;
        private float _jumpForce;

        public void Jump()
        {
            _playerController.VerticalVelocity = _jumpForce;
        }

        //Constructor
        public JumpPerformer(PlayerController playerController,
            float jumpForce)
        {
            _playerController = playerController;
            _jumpForce = jumpForce;
        }
    }

}