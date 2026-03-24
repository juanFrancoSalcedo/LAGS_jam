using DG.Tweening;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;

public class DemoDialogAnimation : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] float timePerChar =0.2f;
    [SerializeField] Ease curve;


    public void ClearText() 
    {
        textComponent.text = "";
    }

#if ANIMA_DOTWEEN_PRO
    public async UniTask AnimateText(ITypingAnimaStrategy animation, string textNew, CancellationToken cancellationToken = default) 
    {
        textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textComponent);
        animation.PreAnimate(animator);
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.3f), cancellationToken: cancellationToken);
        await UniTask.WhenAll(animation.Animate(animator, timePerChar, curve));
    }
#endif
}



