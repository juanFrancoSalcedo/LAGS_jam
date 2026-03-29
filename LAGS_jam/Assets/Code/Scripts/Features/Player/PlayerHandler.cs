using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderSpt = null;
    [SerializeField] private float speed = 2;
    [SerializeField] private Animator animator = null;
    private bool freeze = true;
    PlayerMovementInputs _playerMovement;
    Rigidbody rb;
    public event Action<bool> OnXSpriteChanged;
    public event Action<bool> OnYSpriteChanged;
    InputSystem_Actions _actions;
    Vector2 direction;
    Vector2 lastDirection;
    PlayerStamina _playerStamina;
    public PlayerStamina PlayerStamina => _playerStamina;

    public bool CarryWagon = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _actions = new InputSystem_Actions();
        _playerMovement = new PlayerMovementInputs(_actions);
        _playerStamina = new PlayerStamina();
        _playerMovement.Configure();
        freeze = !(GameStateContext.State == TypeGameState.EnterCave ||
            GameStateContext.State == TypeGameState.StartDay||
            GameStateContext.State == TypeGameState.OutCave 
            );
    }

    public void FreezePlayer(bool _freeze) => freeze = _freeze;

    private IEnumerator Start()
    {
        while (true) 
        {
            yield return new WaitForSeconds(0.1f);
            _playerStamina.RestoreStamina(0.1f);
        }
    }

    #region Stamina
    public void DebtStamina(float amount)
    {
        if(!PlayerStamina.IsExhausted(amount))
            PlayerStamina.DebtStamina(amount);
        else
            AudioManager.Instance.PlaySigh();
    }

    public bool IsExhausted(float amount) => PlayerStamina.IsExhausted(amount);
    #endregion

    private void OnEnable()
    {
        _actions.Enable();
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.StartDay,()=> freeze = false);
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.EndDay, () => freeze = true);
    }

    private void OnDisable()
    {
        _actions.Disable();
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.StartDay, () => freeze = false);
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.EndDay, () => freeze = true);
    }

    private void FixedUpdate()
    {
        if (freeze || animator.GetBool("Mining"))
            return;
        if (direction.x == 0 && direction.y == 0)
        {
            AudioManager.Instance.StopCaveSteps();
            return;
        }
        else
            AudioManager.Instance.PlayCaveSteps();
        rb.linearVelocity = new Vector3(direction.x, -0.98f,direction.y) * speed;

        // keep direction in x axis
        if (direction.x != 0)
        {
            bool before = renderSpt.flipX;
            renderSpt.flipX = lastDirection.x <= 0;
            if (before != renderSpt.flipX)
                OnXSpriteChanged?.Invoke(renderSpt.flipX);
        }
        if (direction.y != 0)
        {
            OnYSpriteChanged?.Invoke(rb.linearVelocity.z >= 0);
        }
    }

    private void Update()
    {

        direction = _playerMovement.Update();

        animator.SetBool("Right", direction.x > 0);
        animator.SetBool("Left", direction.x < 0);
        animator.SetBool("Up", direction.y > 0);
        animator.SetBool("Down", direction.y < 0);

        animator.SetFloat("AxisX", direction.x);
        animator.SetFloat("AxisY", direction.y);
        lastDirection = direction;
    }
}
