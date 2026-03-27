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
        clone.text = $"Renta:{-10}$";
        clone.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone2 = Instantiate(prototypeTextDay, content);
        clone2.text = $"Escula:{-10}$";
        clone2.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone3 = Instantiate(prototypeTextDay, content);
        clone3.text = $"Transporte:{-10}$";
        clone3.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone4 = Instantiate(prototypeTextDay, content);
        clone4.text = $"Mercado:{-10}$";
        clone4.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone5 = Instantiate(prototypeTextDay, content);
        clone5.text = $"Deuda total:{-40}$";
        clone5.transform.SetAsLastSibling();
        yield return new WaitForSeconds(1.2f);
        var clone6 = Instantiate(prototypeTextDay, content);
        clone6.text = $"Mi dinero :{MoneyDataService.GetMoney()} {-40} = {MoneyDataService.GetMoney()-40}$";
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
