using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementDeleteMe : MonoBehaviour
{
    InputSystem_Actions inputActions;

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.FindAction("Attack").performed += Metodo;
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    void Update()
    {
        inputActions.FindAction("Attack").WasPressedThisFrame();

    }

    private void Metodo(InputAction.CallbackContext context)
    {
        print("Presionado");
    }
}
