using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private CardInventoryItem prototypeCard;
    [SerializeField] Transform container;


    private InventoryCardController controller;

    private void Awake()
    {
        controller = new InventoryCardController();
        var data = InventoryDataService.ReadData();
    }
    public void CreateItems(ResourceWrapper wrapper) 
    {
        wrapper.resources.ForEach(i => { 
            var clone = Instantiate(prototypeCard,container);

        });
    }
}


public class InventoryCardPresenter
{ 
    
}

public class InventoryCardController
{
    

}
