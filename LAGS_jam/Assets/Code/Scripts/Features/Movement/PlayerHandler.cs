using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour
{
    InputSystem_Actions inputActions;
    Rigidbody rb;
    [SerializeField] private SpriteRenderer renderSpt = null;

    Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.FindAction("Move").performed += Movement;
        inputActions.FindAction("Move").canceled += EndMovement;
    }


    private void OnDisable()
    {
        inputActions.FindAction("Move").performed -= Movement;
        inputActions.FindAction("Move").canceled -= EndMovement;
    }

    private void FixedUpdate()
    {
        if (direction.x == 0 && direction.y == 0)
            return;
        rb.linearVelocity = new Vector3(direction.x, 0, direction.y);

        // keep direction in x axis
        if (direction.x != 0)
            renderSpt.flipX = rb.linearVelocity.x <= 0;
    }

    private void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    private void EndMovement(InputAction.CallbackContext context)
    {
        direction = Vector2.zero;
    }
}
