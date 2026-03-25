using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private CardInventoryItem prototypeCard;
    [SerializeField] Transform container;
    [SerializeField] Transform scrollViewContent;

    private void OnDisable()
    {
        ClearItems();
    }

    private void OnEnable()
    {
        OpenItems();
    }

    public void OpenItems() 
    {
        var data = InventoryDataService.ReadData();
        print(data.resources.Count);
        CreateItems(data);
    }

    public void CreateItems(ResourceWrapper wrapper) 
    {
        wrapper.resources.ForEach(i => { 
            var clone = Instantiate(prototypeCard, scrollViewContent);
            clone.Configure(i);
            clone.DisplayInfo();
        });
    }

    public void ClearItems() 
    {
        for (int i = 0; i < scrollViewContent.childCount; i++) 
        {
            Destroy(scrollViewContent.GetChild(i).gameObject);
        }
    }
}

