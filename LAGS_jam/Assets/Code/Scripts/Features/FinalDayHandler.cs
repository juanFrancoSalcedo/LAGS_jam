using B_Extensions.SceneLoader;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalDayHandler : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform content;
    [SerializeField] private TMP_Text prototypeTextDay;
    [SerializeField] private AnimationUIController animationUIController;
    [SerializeField] private CallerSceneLoader callerSceneLoader;
    [SerializeField] private Button buttonLoad;
    [Header("-Texts-")]
    [SerializeField] private TMP_Text textRenta;
    [SerializeField] private TMP_Text textEscuela;
    [SerializeField] private TMP_Text textTransporte;
    [SerializeField] private TMP_Text textMercado;
    [SerializeField] private TMP_Text textDeudaTotal;
    [SerializeField] private TMP_Text textMiDinero;
    [SerializeField] private TMP_Text textResult;
    [Header("-Dialogs-")]
    [SerializeField] private DialogSheet dialogRenta;
    [SerializeField] private DialogSheet dialogEscuela;
    [SerializeField] private DialogSheet dialogTransporte;
    [SerializeField] private DialogSheet dialogMercado;
    [SerializeField] private DialogSheet dialogDeudatotal;
    [SerializeField] private DialogSheet dialogMiDinero;

    private void Start()
    {
        buttonLoad.onClick.AddListener(Open);
    }

    private void OnEnable() => GameStateContext.GameStateMediator.Subscribe(TypeGameState.EndDay, OpenEndDay);

    private void OnDisable() => GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.EndDay, OpenEndDay);

    [ContextMenu("OpenDay")]
    public void OpenEndDay() => StartCoroutine(EndDay());

    private IEnumerator EndDay() 
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        textRenta.text = dialogRenta.Model.GetDialog();
        textRenta.transform.DOScaleY(1, 0.5f).From(0).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.2f);
        textEscuela.text = dialogEscuela.Model.GetDialog();
        textEscuela.transform.DOScaleY(1, 0.5f).From(0).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.2f);
        textTransporte.text = dialogTransporte.Model.GetDialog();
        textTransporte.transform.DOScaleY(1, 0.5f).From(0).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.2f);
        textMercado.text = dialogMercado.Model.GetDialog();
        textMercado.transform.DOScaleY(1, 0.5f).From(0).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.2f);
        textDeudaTotal.text = dialogDeudatotal.Model.GetDialog();
        textDeudaTotal.transform.DOScaleY(1, 0.5f).From(0).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.2f);
        textMiDinero.text = dialogMiDinero.Model.GetDialog();
        textMiDinero.transform.DOScaleY(1, 0.5f).From(0).SetEase(Ease.OutBack);
        MoneyDataService.RemoveMoney(40);
        yield return new WaitForSeconds(1.2f);
        textResult.text = MoneyDataService.GetMoney().ToString();
        DayDataService.AddDay();
    }

    public void Open()
    {
        StartCoroutine(LoadNextscene());
    }

    private IEnumerator LoadNextscene() 
    {
        yield return new WaitForSeconds(1f);
        animationUIController.gameObject.SetActive(true);
        callerSceneLoader.LoadScene();
        panel.SetActive(false);
        yield return new WaitForSeconds(1f);
        if (DayDataService.IsLastDay())
            GameStateContext.ChangeState(TypeGameState.FinishGame);
        else
            GameStateContext.ChangeState(TypeGameState.StartDay);
    }


}
