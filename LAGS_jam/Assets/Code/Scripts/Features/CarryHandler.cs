using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryHandler : MonoBehaviour
{
    [SerializeField] TriggerDetector triggerDetector;
    [SerializeField] private PlayerHandler playerHandler;

    List<ResourceHandler> resourceHandlers = new List<ResourceHandler>();
    ResourceHandler carryElement = null;

    InputSystem_Actions inputActions;
    InputAction actionCarry;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        actionCarry = inputActions.FindAction("Interact");
    }

    private void OnEnable()
    {
        triggerDetector.OnTriggerEntered += AddCarryOption;
        triggerDetector.OnTriggerExited += RemoveCarryOption;
        playerHandler.OnXSpriteChanged += ChangedDirectionPick;
        inputActions.Enable();
    }
    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= AddCarryOption;
        triggerDetector.OnTriggerExited -= RemoveCarryOption;
        playerHandler.OnXSpriteChanged -= ChangedDirectionPick;
        inputActions.Disable();
    }

    private void ChangedDirectionPick(bool obj)
    {
        if (obj)
        {
            transform.localPosition = new Vector3(-0.8f, transform.localPosition.y);
            var r = transform.localRotation.eulerAngles;
            r.y = -180;
            transform.rotation = Quaternion.Euler(r);
            //_pickMovement.isFliped = true;
        }
        else
        {
            transform.localPosition = new Vector3(0.8f, transform.localPosition.y);
            var r = transform.localRotation.eulerAngles;
            r.y = 0;
            transform.rotation = Quaternion.Euler(r);
            //_pickMovement.isFliped = false;
        }
    }

    private void Update()
    {
        if (actionCarry.WasPressedThisFrame())
        {
            if(carryElement)
                TryDrop();
            else
                TryCarry();
        }
    }

    private void RemoveCarryOption(Transform _transform)
    {
        if (_transform.TryGetComponent<ResourceHandler>(out var rh))
        {
            resourceHandlers.RemoveAll(_rh => ReferenceEquals(rh,_rh));
        }
    }

    private void AddCarryOption(Transform _transform)
    {
        if (_transform.TryGetComponent<ResourceHandler>(out var rh))
        { 
            if(!resourceHandlers.Contains(rh))
                resourceHandlers.Add(rh);
        }
    }

    public void TryCarry()
    {
        if (resourceHandlers.Count == 0)
            return;
        carryElement = resourceHandlers[0];
        resourceHandlers.RemoveAt(0);
        carryElement.CarryPosition = transform;
        playerHandler.DebtStamina(Constants.ActionDebt);
    }

    private void TryDrop()
    {
        resourceHandlers.Add(carryElement);
        carryElement.CarryPosition = null;
        var wagon = FindAnyObjectByType<Wagon>();
        if (wagon.IsNearPlayer())
        {
            carryElement.AnimateJump(wagon.transform);
            playerHandler.DebtStamina(Constants.ActionDebt);
        }
        carryElement = null;
    }
}
