using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerStaminaView : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Image imageAnimation;
    [SerializeField] PlayerHandler playerHandler;


    private void Start()
    {
        playerHandler = FindFirstObjectByType<PlayerHandler>();
    }

    private void OnEnable()
    {
        PlayerStamina.OnStaminaChanged += DisplayDrain;
        //GameStateContext.GameStateMediator.Subscribe(TypeGameState.StartDay, Animate);
    }

    private void OnDisable()
    {
        PlayerStamina.OnStaminaChanged -= DisplayDrain;
        //GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.StartDay, Animate);
    }


    private void DisplayDrain(float arg1, float arg2, float maxStamina)
    {
        imageFill.fillAmount = (arg1/ maxStamina);
        StartAnimation(arg1 / maxStamina, arg2/maxStamina);
    }

    private void StartAnimation(float now, float before) 
    {
        DOTween.To(() => imageAnimation.fillAmount, juu => imageAnimation.fillAmount = juu, now,
                0.4f);
    }
}
     