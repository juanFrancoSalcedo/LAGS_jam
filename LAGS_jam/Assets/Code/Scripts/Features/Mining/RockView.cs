using System;
using UnityEngine;

public class RockView : MonoBehaviour
{
    [SerializeField] private TriggerDetector _detector;
    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private GameObject _rockVisual;
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private int _hitDamage = 1;

    private RockController _controller;

    public void Initialize(RockController controller)
    {
        _controller = controller;
    }

    private void Start()
    {
        _detector.OnTriggerEntered += HandlePlayerHit;
    }

    private void OnDestroy()
    {
        _detector.OnTriggerEntered -= HandlePlayerHit;
    }

    private void HandlePlayerHit(Transform player)
    {
        _controller.TakeHit(_hitDamage);
    }

    public void PlayHitParticles()
    {
        _hitParticles?.Play();
    }

    public void DestroyRock()
    {
        Destroy(gameObject);
    }
}
