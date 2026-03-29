using UnityEngine;

public class CallerGameState:MonoBehaviour
{
    [SerializeField] private TypeGameState gameState;
    public void CallState()
    {
        GameStateContext.ChangeState(gameState);
    }
}