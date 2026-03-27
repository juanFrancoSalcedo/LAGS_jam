
using UnityEngine;
using DG.Tweening;

public class RockHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitParticles;   
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private ResourceHandler[] resourceHandler;
    [SerializeField] private int amountResourcesInstanciate = 3;

    private void Start()
    {
        _hitParticles.transform.SetParent(null);
    }

    public void Hit()
    {
        _hitParticles?.Play();
        transform.DOShakeScale(0.2f, strength: 0.2f);
    }

    public void MakeDamage() 
    {
        if (_maxHP <= 0)
            return;

        _maxHP--;
        if (_maxHP <= 0)
            DestroyRock();
        else
            Hit();
    }

    public void DestroyRock()
    {
        _maxHP = 0;
        transform.DOScale(0,0.1f).OnComplete(
            () => 
                {
                    int count = 0;
                    while (count<amountResourcesInstanciate) 
                    {
                        count++;
                        Instantiate(resourceHandler[Random.Range(0, resourceHandler.Length)], transform.position, Quaternion.identity);
                    }
                }
            );
        Destroy(gameObject,2);
    }
}
