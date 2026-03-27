using UnityEngine;

public class SingleDialogHandler:MonoBehaviour 
{
    [SerializeField] private DialogSheet dialogSheet;
    [SerializeField] private DialogAnimation dialogAnima;

    public void ShowAnimation() 
    {
        dialogAnima.AnimateDefault(dialogSheet.Model.GetDialog());
    }
}

