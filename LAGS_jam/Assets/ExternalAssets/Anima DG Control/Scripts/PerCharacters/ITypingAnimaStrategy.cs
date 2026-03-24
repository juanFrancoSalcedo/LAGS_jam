using Cysharp.Threading.Tasks;
using DG.Tweening;

public interface ITypingAnimaStrategy
{
    public void PreAnimate(DOTweenTMPAnimator animator);
    public UniTask Animate(DOTweenTMPAnimator animator,float timePerChar, Ease curve);
    public void CleanAnimations(DOTweenTMPAnimator animator);
}