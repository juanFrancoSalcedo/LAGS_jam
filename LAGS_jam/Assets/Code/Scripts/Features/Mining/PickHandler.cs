using System;
using UnityEngine;

public class PickHandler : MonoBehaviour
{
    [SerializeField] private TriggerDetector triggerDetector;

    private void OnEnable()
    {
        triggerDetector.OnTriggerEntered += Pick;
    }

    private void Pick(Transform transform)
    {
        print("Picaar");
    }

    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= Pick;
    }
}
