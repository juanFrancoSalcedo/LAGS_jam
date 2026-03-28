
using UnityEngine;

public class ParticleRandomDelay : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float minDelay = 0.1f;
    [SerializeField] private float maxDelay = 0.5f;
    private void Start()
    {
        if (_particleSystem != null)
        {
            var main = _particleSystem.main;
            main.startDelay = UnityEngine.Random.Range(minDelay, maxDelay);
        }
    }
}
