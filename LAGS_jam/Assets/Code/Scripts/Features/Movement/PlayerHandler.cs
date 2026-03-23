using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderSpt = null;
    [SerializeField] private float speed = 2;
    PlayerMovementInputs _playerMovement;
    Rigidbody rb;
    public event Action<bool> OnXSpriteChanged;

    Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _playerMovement = new PlayerMovementInputs();
        _playerMovement.Configure();
    }

    private void FixedUpdate()
    {
        if (direction.x == 0 && direction.y == 0)
            return;
        
        rb.linearVelocity = new Vector3(direction.x, 0,direction.y) * speed;

        // keep direction in x axis
        if (direction.x != 0)
        {
            bool before = renderSpt.flipX;
            renderSpt.flipX = rb.linearVelocity.x <= 0;
            if (before != renderSpt.flipX)
                OnXSpriteChanged?.Invoke(renderSpt.flipX);
        }
    }

    private void Update() => direction = _playerMovement.Update();
}


public class PlayerMovementInputs
{
    InputSystem_Actions inputActions;
    InputAction actionMove;

    ~PlayerMovementInputs() 
    {
        inputActions.Disable();
    }

    public void Configure() 
    {
        inputActions = new InputSystem_Actions();
        actionMove = inputActions.FindAction("Move");
        inputActions.Enable();
    }

    public Vector2 Update() 
    {
        return actionMove.ReadValue<Vector2>();
    }
}