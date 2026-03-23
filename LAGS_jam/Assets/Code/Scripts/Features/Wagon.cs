using System;
using UnityEngine;

public class Wagon:MonoBehaviour 
{
    [SerializeField] private float distanceCollect = 4f;
    [SerializeField] TriggerDetector triggerDetector;
    PlayerHandler playerHandler;

    private void Start()
    {
        playerHandler = GameObject.FindWithTag("Player").GetComponent<PlayerHandler>();
    }


    public bool IsNearPlayer() 
    {
        return Vector3.Distance(playerHandler.transform.position,transform.position) < distanceCollect;
    }

    private void OnEnable() => triggerDetector.OnTriggerEntered += Collect;
    private void OnDisable() => triggerDetector.OnTriggerEntered -= Collect;
    private void Collect(Transform _transform)
    {
        _transform.gameObject.SetActive(false);
        MiningMediator.Publish(TypeMiningEvent.CollectResource);
        InventoryMediator.Publish(TypeResource.EmeraldCristals);
    }
}
