using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputs
{
    InputAction actionMove;
    InputSystem_Actions inputActions;

    public PlayerMovementInputs(InputSystem_Actions inputActions)
    {
        this.inputActions = inputActions;
    }

    public void Configure() 
    {
        actionMove = inputActions.FindAction("Move");
    }

    public Vector2 Update() 
    {
        return actionMove.ReadValue<Vector2>();
    }
}