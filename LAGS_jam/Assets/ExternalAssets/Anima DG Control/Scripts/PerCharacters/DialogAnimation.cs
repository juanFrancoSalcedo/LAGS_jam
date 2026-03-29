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

    public void AnimateDefault(string dialog) 
    {
        AnimCharHorizon animationDefault = new AnimCharHorizon();
        textComponent.text = dialog;
        //if (animationDefault == null)
        //{
        //    print("animationDefault null");
        //}
        //StartAnimateCoroutine(dialog);
        //await AnimateText(animationDefault, dialog);
    }

    public async UniTask AnimateText(ITypingAnimaStrategy animation, string textNew) 
    {
        textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textComponent);
        if (animator == null) 
        {
            print("animator null");
        }

        animation.PreAnimate(animator);
        if (animator == null)
            print("animation null");
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.3f));
        await UniTask.WhenAll(animation.Animate(animator, timePerChar, curve));
    }

    /// <summary>
    /// Versión con corrutina de AnimateText
    /// </summary>
    public IEnumerator AnimateTextCoroutine(ITypingAnimaStrategy animation, string textNew)
    {
        textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textComponent);
        
        if (animator == null) 
        {
            print("animator null");
            yield break;
        }

        // Pre-animación (configuración inicial)
        animation.PreAnimate(animator);
        
        // Delay inicial de 0.3 segundos
        yield return new WaitForSeconds(0.3f);
        
        // Obtener el UniTask de la animación
        UniTask animationTask = animation.Animate(animator, timePerChar, curve);
        
        // Esperar a que la animación termine usando yield
        yield return animationTask.ToCoroutine();
    }

    /// <summary>
    /// Versión simplificada con corrutina usando DOTween directamente
    /// </summary>
    public IEnumerator AnimateTextCoroutineSimple(string textNew)
    {
        textComponent.text = textNew;
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(textComponent);
        
        if (animator == null) 
        {
            print("animator null");
            yield break;
        }

        // Crear la animación por defecto (AnimCharHorizon)
        AnimCharHorizon animation = new AnimCharHorizon();
        animation.PreAnimate(animator);
        
        // Delay inicial
        yield return new WaitForSeconds(0.3f);
        
        // Crear la secuencia de DOTween
        Sequence sequence = DOTween.Sequence();
        Sequence sequence2 = DOTween.Sequence();
        print("Animando caracteres..."+animator == null);
        print("Animando caracteres..." + animator.textInfo == null);

        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible)
                continue;
                
            Vector3 currCharOffset = animator.GetCharOffset(i);
            sequence.Append(animator.DOOffsetChar(i, currCharOffset - new Vector3(30, 0, 0), timePerChar));
            sequence2.Append(animator.DOFadeChar(i, 1, timePerChar));
        }
        
        // Esperar a que la secuencia termine
        yield return sequence.WaitForCompletion();
    }

    /// <summary>
    /// Iniciar la animación con corrutina desde código
    /// </summary>
    public void StartAnimateCoroutine(string message)
    {
        StartCoroutine(AnimateTextCoroutineSimple(message));
    }

#endif
}

