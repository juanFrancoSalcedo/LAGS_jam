using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LightTransport;


[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderSpt = null;
    [SerializeField] private float speed = 2;
    InputSystem_Actions inputActions;
    InputAction actionMove;
    Rigidbody rb;
    public event System.Action<bool> OnXSpriteChanged;

    Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
        actionMove = inputActions.FindAction("Move");

    }
    void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        if (direction.x == 0 && direction.y == 0)
            return;
        
        rb.linearVelocity = new Vector3(
            direction.x
            , 0,
            direction.y) * speed;

        // keep direction in x axis
        if (direction.x != 0)
        {
            bool before = renderSpt.flipX;
            renderSpt.flipX = rb.linearVelocity.x <= 0;
            if (before != renderSpt.flipX)
                OnXSpriteChanged?.Invoke(renderSpt.flipX);
        }
    }

    private void Update()
    {
        direction = actionMove.ReadValue<Vector2>();
    }
}
