using B_Extensions.SceneLoader;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FinalDayHandler : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform content;
    [SerializeField] private TMP_Text prototypeTextDay;
    [SerializeField] private AnimationUIController animationUIController;
    [SerializeField] private CallerSceneLoader callerSceneLoader;
    [Header("-Dialogs-")]
    [SerializeField] private DialogSheet dialogRenta;
    [SerializeField] private DialogSheet dialogEscuela;
    [SerializeField] private DialogSheet dialogTransporte;
    [SerializeField] private DialogSheet dialogMercado;
    [SerializeField] private DialogSheet dialogDeudatotal;
    [SerializeField] private DialogSheet dialogMiDinero;

    private void OnEnable() => GameStateContext.GameStateMediator.Subscribe(TypeGameState.EndDay, OpenEndDay);

    private void OnDisable() => GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.EndDay, OpenEndDay);

    [ContextMenu("OpenDay")]
    public void OpenEndDay() => StartCoroutine(EndDay());

    private IEnumerator EndDay() 
    {
        panel.SetActive(true);
        ClearTexts();
        yield return new WaitForSeconds(1.2f);
        var clone = Instantiate(prototypeTextDay, content);
        clone.text = dialogRenta.Model.GetDialog().Replace("#", "-10");//$"Renta:{-10}$";
        clone.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone2 = Instantiate(prototypeTextDay, content);
        clone2.text = dialogEscuela.Model.GetDialog().Replace("#", "-10");
        clone2.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone3 = Instantiate(prototypeTextDay, content);
        clone3.text = dialogTransporte.Model.GetDialog().Replace("#", "-10");
        clone3.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone4 = Instantiate(prototypeTextDay, content);
        clone4.text = dialogMercado.Model.GetDialog().Replace("#", "-10");
        clone4.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone5 = Instantiate(prototypeTextDay, content);
        clone5.text = dialogDeudatotal.Model.GetDialog().Replace("#","-40");
        clone5.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone6 = Instantiate(prototypeTextDay, content);
        clone6.text = dialogMiDinero.Model.GetDialog().Replace("#",$"{MoneyDataService.GetMoney()} {-40} = {MoneyDataService.GetMoney() - 40}");
        clone6.transform.SetAsLastSibling();
        MoneyDataService.RemoveMoney(40);
        yield return new WaitForSeconds(1.2f);
        DayDataService.AddDay();
        animationUIController.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        callerSceneLoader.LoadScene();
        panel.SetActive(false);
        yield return new WaitForSeconds(1f);
        if(DayDataService.IsLastDay())
            GameStateContext.ChangeState(TypeGameState.FinishGame);
        else
            GameStateContext.ChangeState(TypeGameState.StartDay);
    }

    private void ClearTexts()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
