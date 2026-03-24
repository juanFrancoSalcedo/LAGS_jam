using TMPro;
using UnityEngine;

public class CardInventoryItem: MonoBehaviour
{

    [SerializeField] private TMP_Text textName;

    ResourceModel model;
    private void Configure(ResourceModel model) 
    {
        this.model = model;
    }

    private void DisplayInfo() 
    {
        
    }
}
