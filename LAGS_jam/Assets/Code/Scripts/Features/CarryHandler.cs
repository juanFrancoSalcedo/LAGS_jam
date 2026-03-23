using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Cinemachine.CinemachineTriggerAction.ActionSettings;

public class CarryHandler : MonoBehaviour
{
    [SerializeField] TriggerDetector triggerDetector;
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
        inputActions.Enable();
    }
    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= AddCarryOption;
        triggerDetector.OnTriggerExited -= RemoveCarryOption;
        inputActions.Disable();
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
            resourceHandlers.Remove(rh);
        }
    }

    private void AddCarryOption(Transform _transform)
    {
        if (_transform.TryGetComponent<ResourceHandler>(out var rh))
        { 
            resourceHandlers.Add(rh);
        }
    }

    public void TryCarry()
    {
        carryElement = resourceHandlers[0];
        resourceHandlers.RemoveAt(0);
    }

    private void TryDrop()
    {
        resourceHandlers.Add(carryElement);
        carryElement = null;
        print("TODO if wagon is near");
    }
}


