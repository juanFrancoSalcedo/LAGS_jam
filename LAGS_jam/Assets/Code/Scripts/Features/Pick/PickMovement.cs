using DG.Tweening;
using UnityEngine;

public class PickMovement 
{
    Transform tool;
    TriggerDetector detector;
    public bool isFliped;
    public bool animating;
    Animator _animator;

    public PickMovement(Transform tool, TriggerDetector detector, Animator animator)
    {
        this.tool = tool;
        this.detector = detector;
        _animator = animator;
    }

    public void Animate() 
    {
        if (animating == true)
            return;
        _animator.SetBool("Mining",true);
        _animator.SetTrigger("MiningTrigger");
        AudioManager.Instance.PlaySwingPickAxe();
        Sequence sequence = DOTween.Sequence();

        animating = true;
        sequence.Append(
            tool.DOLocalRotate(new Vector3(0, isFliped ? -180 : 0, 90),0.3f).OnComplete(ActivePickCollision)
        );

        sequence.Append(
            tool.DOLocalRotate(new Vector3(0, isFliped ? -180 : 0, 0), 0.2f).OnComplete(DeactivePickCollision)
        ).OnComplete(()=>_animator.SetBool("Mining", false));
    }

    private void ActivePickCollision()
    {
        detector.gameObject.SetActive(true);
    }

    private void DeactivePickCollision()
    {
        detector.gameObject.SetActive(false);
        animating = false;
    }
}
