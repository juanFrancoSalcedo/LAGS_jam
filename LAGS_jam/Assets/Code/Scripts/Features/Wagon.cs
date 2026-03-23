using System;
using UnityEngine;

public class Wagon:MonoBehaviour 
{
    [SerializeField] private float distanceCollect = 4f;
    [SerializeField] TriggerDetector triggerDetector;
    PlayerHandler playerHandler;

    private void Start() => playerHandler = GameObject.FindWithTag("Player").GetComponent<PlayerHandler>();

    public bool IsNearPlayer() 
    {
        return Vector3.Distance(playerHandler.transform.position,transform.position) < distanceCollect;
    }

    private void OnEnable() => triggerDetector.OnTriggerEntered += Collect;
    private void OnDisable() => triggerDetector.OnTriggerEntered -= Collect;
    private void Collect(Transform _transform)
    {
        if(_transform.TryGetComponent<ResourceHandler>(out var compo))
        {
            print("TODO Inject this perrito, shutese esto mi socio");
            _transform.gameObject.SetActive(false);
            MiningMediator.Publish(TypeMiningEvent.CollectResource);
            InventoryDataService.AddItem(compo.Sheet.GetModelCopy());
        }
    }
}
