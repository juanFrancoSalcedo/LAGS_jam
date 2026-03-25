using B_Extensions;
using UnityEngine;

public class ButtonAcceptTrade:BaseButtonAttendant 
{
    [SerializeField] TraderHandler npcHandler;
    [SerializeField] private bool Accept;
    private void Start() => buttonComponent.onClick.AddListener(TryAccept);

    private void TryAccept() => npcHandler.AcceptTrade(Accept);
}
