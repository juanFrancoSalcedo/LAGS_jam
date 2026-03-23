using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ResourceHandler : MonoBehaviour
{ 
    Rigidbody rb;
    
    public Transform CarryPosition { get; set; }

    bool previouseHasCarry;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        AnimateInstanciate();
    }

    private void Update()
    {
        if (previouseHasCarry && CarryPosition == null)
        {
            AnimateInstanciate();
            previouseHasCarry = false;
        }


        if(CarryPosition != null)
        { 
            transform.position = CarryPosition.position;
            previouseHasCarry = true;
        }
    }

    public void AnimateJump(Transform positionWagon) 
    {
        transform.DOJump(positionWagon.position, 1, 1, 0.5f);
    }

    public void AnimateInstanciate() 
    {
        rb.AddForce((Vector3.up*5)+ (Vector3.right * Random.Range(-2, 2)), ForceMode.Impulse);
    }
}