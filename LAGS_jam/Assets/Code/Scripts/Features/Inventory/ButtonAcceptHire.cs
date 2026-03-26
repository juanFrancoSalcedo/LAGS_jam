using B_Extensions;
using UnityEngine;

public class ButtonAcceptHire : BaseButtonAttendant
{
    [SerializeField] HireHandler npcHandler;
    [SerializeField] private bool Accept;
    private void Start() => buttonComponent.onClick.AddListener(TryAccept);

    private void TryAccept() => npcHandler.AcceptHire(Accept);
}
