
using UnityEngine;

public class AnimationRandomDelay:MonoBehaviour
{
    Animation _animation;
    [SerializeField] private float minDelay = 0.1f;
    [SerializeField] private float maxDelay = 0.5f;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        if (_animation != null)
        {
            Invoke(nameof(PlayAnimation),Random.Range(minDelay,maxDelay));
        }
    }

    private void PlayAnimation()
    {
        _animation.Play();
    }
}