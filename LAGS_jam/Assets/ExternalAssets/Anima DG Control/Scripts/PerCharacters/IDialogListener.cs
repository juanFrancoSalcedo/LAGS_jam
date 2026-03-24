using System;

public interface IDialogListener
{
    public Action OnDialogStarted { get; set; }
    public Action<int,int> OnDialogUpdate { get; set; }
    public Action OnDialogComplete { get; set;}
}