using B_Extensions;
using UnityEngine;
using Zenject;

public class ButtonAcceptTrade:BaseButtonAttendant 
{
    [Inject] TraderHandler npcHandler;
    [SerializeField] private bool Accept;
    private void Start() => buttonComponent.onClick.AddListener(TryAccept);

    private void TryAccept() => npcHandler.AcceptTrade(Accept);
}
