
using UnityEngine;
using DG.Tweening;

public class RockHandler : MonoBehaviour
{
    [SerializeField] private TriggerDetector _detector;
    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private GameObject _rockVisual;
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private ResourceHandler[] resourceHandler;

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
        print("Muero");
        transform.DOScale(0,0.1f).OnComplete(
            () => 
                {
                    Instantiate(resourceHandler[Random.Range(0,resourceHandler.Length)],transform.position, Quaternion.identity);
                    Instantiate(resourceHandler[Random.Range(0, resourceHandler.Length)], transform.position, Quaternion.identity);
                    Instantiate(resourceHandler[Random.Range(0, resourceHandler.Length)], transform.position, Quaternion.identity);
                }
            );
        Destroy(gameObject,2);
    }
}
