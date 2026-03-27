using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineCamera camFollow;
    [SerializeField] CinemachineCamera camPresentation;
    [SerializeField] CinemachineBrain cinemachineBrain;
    private void Awake()
    {
        switch (GameStateContext.State)
        {
            case TypeGameState.None:
                break;
            case TypeGameState.Welcome:
                WelcomeCamera();
                break;
            case TypeGameState.StartDay:
                StartFollowCam();
                break;
            case TypeGameState.EndDay:
                break;
            case TypeGameState.FinishGame:
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.Welcome, WelcomeCamera);
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.StartDay, StartFollowCam);
    }


    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.Welcome, WelcomeCamera);
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.StartDay, StartFollowCam);
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
