using B_Extensions.SceneLoader;
using System;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] TriggerDetector triggerDetector;
    [SerializeField] CallerSceneLoader callerSceneLoader;

    private void OnEnable()
    {
        triggerDetector.OnTriggerStayed += SumChangeScene;
        triggerDetector.OnTriggerExited += ExitExit;
    }

    private void OnDisable()
    {
        triggerDetector.OnTriggerStayed -= SumChangeScene;
        triggerDetector.OnTriggerExited -= ExitExit;
    }

    private void ExitExit(Transform transform) => sum = 0;

    private void SumChangeScene(Transform transform)
    {
        sum++;
        CheckLoad();
    }
    bool loaded;

    int sum = 0;
    private void CheckLoad() 
    {
        if (sum > 3 && !loaded)
        {
            callerSceneLoader.LoadScene();
            loaded = true;
        }
    }
}
