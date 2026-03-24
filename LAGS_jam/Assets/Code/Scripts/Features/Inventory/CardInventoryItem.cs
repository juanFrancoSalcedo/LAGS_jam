using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryItem: MonoBehaviour
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textQuality;
    [SerializeField] private Image imageIcon;
    [SerializeField] private Button buttonCheckValue;
    public static event System.Action<ResourceModel> OnTryTrade;

    private void Start()
    {
        buttonCheckValue.onClick.AddListener(CallTrader);
    }

    private void CallTrader()
    {
        OnTryTrade?.Invoke(model);
    }

    ResourceModel model;
    public void Configure(ResourceModel model) 
    {
        this.model = model;
    }

    public void DisplayInfo() 
    {
        textName.text = model.Name;
        textQuality.text = model.Quality.ToString();
        imageIcon.sprite = ResourcesRepository.Instance.GetSpriteByNameAndQuality(model.Name,model.Quality);
    }
}