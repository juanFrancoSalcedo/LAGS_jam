using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineCamera camFollow;
    [SerializeField] CinemachineCamera camPresentation;
    [SerializeField] CinemachineBrain cinemachineBrain;

    private void OnEnable()
    {
        GameStateMediator.Subscribe(TypeGameState.Welcome, WelcomeCamera);
        GameStateMediator.Subscribe(TypeGameState.StartDay, StartFollowCam);
    }


    private void OnDisable()
    {
        GameStateMediator.Unsubscribe(TypeGameState.Welcome, WelcomeCamera);
        GameStateMediator.Subscribe(TypeGameState.StartDay, StartFollowCam);
    }

    private void StartFollowCam()
    {
        camFollow.Priority = 1;
        camPresentation.Priority = 0;
    }

    private void WelcomeCamera()
    {
        camFollow.Priority = 0;
        camPresentation.Priority = 1;
    }
}
