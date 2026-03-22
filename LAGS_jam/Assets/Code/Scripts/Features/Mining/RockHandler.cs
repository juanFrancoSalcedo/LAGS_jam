using System;
using UnityEngine;

public class RockHandler : MonoBehaviour
{
    [SerializeField] private TriggerDetector _detector;
    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private GameObject _rockVisual;
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private int _hitDamage = 1;



    public void PlayHitParticles()
    {
        _hitParticles?.Play();
    }

    public void DestroyRock()
    {
        Destroy(gameObject);
    }
}
