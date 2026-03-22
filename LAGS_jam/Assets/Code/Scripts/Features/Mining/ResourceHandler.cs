
using DG.Tweening;
using System;
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
    }

    public void AnimateInstanciate() 
    {
        rb.AddForce(Vector3.up*3, ForceMode.Impulse);
    }
}