
using UnityEngine;
using DG.Tweening;

public class RockHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitParticles;   
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private ResourceHandler[] resourceHandler;
    [SerializeField] private int amountResourcesInstanciate = 3;
    [SerializeField] Animator animator;

    private void Start()
    {
        _hitParticles.transform.SetParent(null);
    }

    public void Hit(bool applyAnimation = true)
    {
        _hitParticles?.Play();
        if(applyAnimation)
            transform.DOShakeScale(0.2f, strength: 0.2f);
        animator.SetTrigger("Mining");
        AudioManager.Instance.PlayHitRock(transform);
    }

    public void MakeDamage() 
    {
        if (_maxHP <= 0)
            return;
        _maxHP--;
        print(_maxHP);
        animator.SetInteger("Life", _maxHP);
        if (_maxHP <= 0)
        {
            Hit(false);
            DestroyRock();
        }
        else
            Hit();
    }

    public void DestroyRock()
    {
        _maxHP = 0;
        transform.DOScale(1,0.3f).OnComplete(
            () => 
                {
                    int count = 0;
                    while (count<amountResourcesInstanciate) 
                    {
                        count++;
                        Instantiate(resourceHandler[UnityEngine.Random.Range(0, resourceHandler.Length)], transform.position, Quaternion.identity);
                    }
                }
            );
        Destroy(gameObject,0.6f);
    }
}
