using System;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class TextResources : MonoBehaviour
{
    [SerializeField] private TypeResource typeResoruce;
    TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        InventoryMediator.Subscribe(typeResoruce,ShowText);
    }


    private void OnDisable()
    {
        InventoryMediator.Unsubscribe(typeResoruce, ShowText);
    }
    private void ShowText()
    {
        Invoke(nameof(DoShow),0.8f);
    }
    private void DoShow() 
    {
        textComponent.text = PlayerPrefs.GetInt(KeyStorage.Count_emerald,0).ToString();
    }
}
