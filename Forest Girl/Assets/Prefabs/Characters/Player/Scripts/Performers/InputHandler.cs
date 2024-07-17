using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler
{
    #region Events
    public UnityEvent OnCrouch { get; private set; } = new UnityEvent();
    public UnityEvent OnJump { get; private set; } = new UnityEvent();
    public UnityEvent OnInteract { get; private set; } = new UnityEvent();
    public UnityEvent OnThrow { get; private set; } = new UnityEvent();
    public UnityEvent OnFirst { get; private set; } = new UnityEvent();
    public UnityEvent OnSecond { get; private set; } = new UnityEvent();
    public UnityEvent OnThird { get; private set; } = new UnityEvent();
    public UnityEvent OnFourth { get; private set; } = new UnityEvent();
    public UnityEvent OnFifth { get; private set; } = new UnityEvent();
    #endregion

    public bool IsCrouching { get { return _actionAssets.Player.Crouch.IsInProgress(); } set { } }
    public bool IsRunning { get { return _actionAssets.Player.Run.IsInProgress(); } set { } }

    private InputActionsAsset _actionAssets;

    //Public Methods
    public Vector2 GetMoveDir()
    { return _actionAssets.Player.Move.ReadValue<Vector2>(); }

    public Vector2 GetLookDir()
    { return new Vector2(_actionAssets.Player.MouseX.ReadValue<float>(), _actionAssets.Player.MouseY.ReadValue<float>()); }

    //Private Methods
    private void CrouchDown(InputAction.CallbackContext context)
    { OnCrouch.Invoke(); }

    private void JumpDown(InputAction.CallbackContext context)
    { OnJump.Invoke(); }

    private void Interact(InputAction.CallbackContext context)
    { OnInteract.Invoke(); }

    private void Throw(InputAction.CallbackContext context)
    { OnThrow.Invoke(); }

    #region InventorySlots
    private void First(InputAction.CallbackContext context)
    { OnFirst.Invoke(); }
    private void Second(InputAction.CallbackContext context)
    { OnSecond.Invoke(); }
    private void Third(InputAction.CallbackContext context)
    { OnThird.Invoke(); }
    private void Fourth(InputAction.CallbackContext context)
    { OnFourth.Invoke(); }
    private void Fifth(InputAction.CallbackContext context)
    { OnFifth.Invoke(); }
    #endregion

    //Constructor
    public InputHandler(InputActionsAsset actionAssets)
    {
        _actionAssets = actionAssets;

        _actionAssets.Player.Crouch.started += CrouchDown;
        _actionAssets.Player.Jump.started += JumpDown;
        _actionAssets.Player.Interact.started += Interact;
        _actionAssets.Player.Throw.started += Throw;
        //Inventory Slots
        _actionAssets.Player._1.started += First;
        _actionAssets.Player._2.started += Second;
        _actionAssets.Player._3.started += Third;
        _actionAssets.Player._4.started += Fourth;
        _actionAssets.Player._5.started += Fifth;
    }
}
