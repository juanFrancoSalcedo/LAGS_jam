using B_Extensions;
using UnityEngine;

public class ButtonAcceptHire : BaseButtonAttendant
{
    [SerializeField] private bool Accept;
    public HireHandler npcHandler;
    private void Start() => buttonComponent.onClick.AddListener(TryAccept);

    private void TryAccept() => npcHandler.AcceptHire(Accept);
}
