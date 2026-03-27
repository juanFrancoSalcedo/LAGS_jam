using TMPro;
using UnityEngine;

public class WeekHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class TextWeek : MonoBehaviour
{
    [SerializeField] string label = "Día: @";
    TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        
    }

    //private void OnEnable()
    //{
    //    MoneyDataService.OnMoneyChanged += OnMoneyChanged;
    //}
    //private void OnDisable()
    //{
    //    MoneyDataService.OnMoneyChanged -= OnMoneyChanged;
    //}

    private void OnMoneyChanged(int arg1, int arg2)
    {
        Invoke(nameof(ShowMoney), 2f);
    }

    private void ShowMoney()
    {
        textComponent.text = label.Replace("@", MoneyDataService.GetMoney().ToString());
    }
}

public class DayDataService 
{
    int currentDay = 1;
    public bool IsLastDay()
    {
        return currentDay>1;
    }
}