using DG.Tweening;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;

public class DialogAnimation : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] float timePerChar =0.2f;
    [SerializeField] Ease curve;

    public void ClearText() 
    {
        textComponent.text = string.Empty;
    }

#if ANIMA_DOTWEEN_PRO

    public async void AnimateDefault(string dialog) 
    {
        AnimCharScaleFade animationDefault = new AnimCharScaleFade();
        await AnimateText(animationDefault, dialog);

    }
    public async UniTask AnimateText(ITypingAnimaStrategy animation, string textNew) 
    {
        textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textComponent);
        animation.PreAnimate(animator);
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.3f)); // Reemplaza WaitForSeconds con UniTask
        await UniTask.WhenAll(animation.Animate(animator, timePerChar, curve));
    }
#endif
}

