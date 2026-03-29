using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryItem: MonoBehaviour
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textQuality;
    [SerializeField] private Image imageIcon;
    [SerializeField] private Transform containerIcon;
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

    public void Configure(ResourceModel model) => this.model = model;

    public void DisplayInfo() 
    {
        textName.text = model.Name;
        textQuality.text = model.Quality.ToString();
        imageIcon.sprite = ResourcesRepository.Instance.GetSpriteByNameAndQuality(model.Name, model.Quality);
        
        var compo = ResourcesRepository.Instance.GetResourcesRandom();

        // Usar Path.Combine para unir paths de manera multiplataforma
        string resourcePath = compo.Path;//Path.Combine("Prototypes/Icons_UI", compo.Path);
        print(resourcePath);
        
        // Resources.Load solo acepta forward slashes, así que normalizamos el path
        resourcePath = resourcePath.Replace(Path.DirectorySeparatorChar, '/');
        
        var reference = Resources.Load<GameObject>(resourcePath);
        
        if (reference == null)
        {
            Debug.LogWarning($"No se pudo cargar el recurso en: {resourcePath}");
        }
        var clone = Instantiate(reference, containerIcon);
        clone.transform.localScale = Vector3.one*0.5f;
    }
}