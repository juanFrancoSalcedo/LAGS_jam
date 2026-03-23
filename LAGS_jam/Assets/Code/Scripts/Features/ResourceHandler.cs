using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ResourceHandler : MonoBehaviour
{ 
    Rigidbody rb;
    CollisionDetector collisionDetector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collisionDetector = GetComponent<CollisionDetector>();
        AnimateInstanciate();
    }

    private void OnEnable()
    {
        collisionDetector.OnCollisionEntered += Collect;
    }

    private void OnDisable()
    {
        collisionDetector.OnCollisionEntered -= Collect;
    }
    private void Collect(Collision collision)
    {
        gameObject.SetActive(false);
        MiningMediator.Publish(TypeMiningEvent.CollectResource);
        InventoryMediator.Publish(TypeResource.EmeraldCristals);
    }

    public void AnimateInstanciate() 
    {
        rb.AddForce((Vector3.up*5)+ (Vector3.right * Random.Range(-2, 2)), ForceMode.Impulse);
    }
}