using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickHandler : MonoBehaviour
{
    [SerializeField] private TriggerDetector triggerDetector;
    [SerializeField] private PlayerHandler playerHandler;
    [SerializeField] private Animator _animator;

    PickMovement _pickMovement;
    InputSystem_Actions inputActions;
    InputAction action;

    private void Awake()
    {
        _pickMovement = new PickMovement(transform,triggerDetector,_animator);
        inputActions = new InputSystem_Actions();
        action = inputActions.FindAction("Jump");
        ChangedDirectionPick(false);
    }

    private void OnEnable()
    {
        triggerDetector.OnTriggerEntered += TriggerPick;
        playerHandler.OnXSpriteChanged += ChangedDirectionPick;
        inputActions.Enable();
    }


    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= TriggerPick;
        playerHandler.OnXSpriteChanged -= ChangedDirectionPick;
        inputActions.Disable();
    }
    private void ChangedDirectionPick(bool obj)
    {
        if (obj)
        {
            transform.localPosition = new Vector3(-0.7f, transform.localPosition.y);
            var r = transform.localRotation.eulerAngles;
            r.y = -180;
            transform.rotation = Quaternion.Euler(r);
            _pickMovement.isFliped = true;
        }
        else 
        {
            transform.localPosition = new Vector3(0.7f, transform.localPosition.y);
            var r = transform.localRotation.eulerAngles;
            r.y = 0;
            transform.rotation = Quaternion.Euler(r);
            _pickMovement.isFliped = false;
        }
    }

    private void TriggerPick(Transform _transform)
    {
        if(_transform.TryGetComponent<RockHandler>(out var compo))
        {
            compo.MakeDamage();
            playerHandler.DebtStamina(2.5f);
        }
    }

    private void Update()
    {
        if (action.WasPressedThisFrame())
        {
            if(!playerHandler.PlayerStamina.IsExhausted(2.5f))
                _pickMovement.Animate();
            else
                AudioManager.Instance.PlaySigh();
        }
    }
}
