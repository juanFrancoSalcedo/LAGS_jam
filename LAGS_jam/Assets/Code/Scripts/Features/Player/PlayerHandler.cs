using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderSpt = null;
    [SerializeField] private float speed = 2;
    PlayerMovementInputs _playerMovement;
    Rigidbody rb;
    public event Action<bool> OnXSpriteChanged;
    InputSystem_Actions _actions;
    Vector2 direction;
    PlayerStamina _playerStamina;
    public PlayerStamina PlayerStamina => _playerStamina;
    private bool freeze = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _actions = new InputSystem_Actions();
        _playerMovement = new PlayerMovementInputs(_actions);
        _playerStamina = new PlayerStamina();
        _playerMovement.Configure();

    }

    #region Controller
    public void DebtStamina(float amount) => PlayerStamina.DebtStamina(amount);
    #endregion

    private void OnEnable()
    {
        _actions.Enable();
        GameStateMediator.Subscribe(TypeGameState.StartDay,()=> freeze = false);
    }

    private void OnDisable()
    {
        _actions.Disable();
        GameStateMediator.Unsubscribe(TypeGameState.StartDay, () => freeze = false);
    }

    private void FixedUpdate()
    {
        if (freeze)
            return;

        if (direction.x == 0 && direction.y == 0)
            return;
        
        rb.linearVelocity = new Vector3(direction.x, -0.98f,direction.y) * speed;

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
