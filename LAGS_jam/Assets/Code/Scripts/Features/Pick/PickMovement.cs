using DG.Tweening;
using UnityEngine;

public class PickMovement 
{
    Transform tool;
    TriggerDetector detector;
    public bool isFliped;
    public bool animating;

    public PickMovement(Transform tool, TriggerDetector detector)
    {
        this.tool = tool;
        this.detector = detector;
    }

    public void Animate() 
    {
        if (animating == true)
            return;

        Sequence sequence = DOTween.Sequence();

        animating = true;
        sequence.Append(
            tool.DOLocalRotate(new Vector3(0, isFliped ? -180 : 0, 90),0.3f).OnComplete(ActivePickCollision)
        );

        sequence.Append(
            tool.DOLocalRotate(new Vector3(0,isFliped?-180:0, 0), 0.2f).OnComplete(DeactivePickCollision)
        );
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
