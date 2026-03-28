using TMPro;
using UnityEngine;

public class TextWeek : MonoBehaviour
{
    [SerializeField] string label = "Día: @";
    TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        ShowDay();
    }

    private void OnEnable() => DayDataService.OnDayChanged += (b, i) => ShowDay();

    private void OnDisable() => DayDataService.OnDayChanged -= (b, i) => ShowDay();

    private void ShowDay()
    {
        textComponent.text = label.Replace("@", DayDataService.GetCurrentDay().ToString());
    }
}

