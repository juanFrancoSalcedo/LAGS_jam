using B_Extensions;
using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class ButtonNextDialog : BaseButtonAttendant,IDialogListener
{
    [SerializeField] DialogManager dialogManager;
    [SerializeField] AnimationUIController animaController;
    CanvasGroup canvasGroup;
    InputSystem_Actions actions;
    TMP_Text textInner;
    public Action OnDialogComplete { get; set; }
    public Action OnDialogStarted { get; set; }
    public Action<int, int> OnDialogUpdate { get; set; }
    private Vector2 initPos;

    private void Awake()
    {
        textInner = GetComponentInChildren<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        actions = new InputSystem_Actions();
        DialogManager.Instance.InjectListener(this);
        initPos = transform.localPosition;
    }

    private void OnEnable()
    {
        actions.Enable();
        OnDialogComplete += DetectEnds;
        OnDialogStarted += DetectStart;
        OnDialogUpdate += (i, f) => ResetInteractable();
    }
    private void OnDisable()
    {
        actions.Disable();
        OnDialogComplete -= DetectEnds;
        OnDialogStarted -= DetectStart;
        OnDialogUpdate -= (i, f) => ResetInteractable();
    }

    private bool Started;
    private void DetectStart() => Started = true;

    private void DetectEnds()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        Started = false;
        ResetAnimation();
    }

    private void ResetInteractable() 
    {
        canvasGroup.interactable = true;
        textInner.text = "Siguiente";
    }


    private void Update()
    {
        if (InteractIconService.Instance.ShowingIcon)
        {
            if (!Started)
            {
                InitState();
            }
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            return;
        }

        if(actions.FindAction("Interact").WasPressedThisFrame())
            NextDialog();
    }

    public void InitState() 
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        textInner.text = "Hablar";
        transform.localPosition = initPos;
    }

    private void Start() => buttonComponent.onClick.AddListener(NextDialog);

    private void NextDialog()
    {
        if(!Started)
            animaController.ActiveAnimation(0);
        dialogManager.TryDialog();
        canvasGroup.interactable = false;
    }

    public void ResetAnimation() => animaController.ActiveAnimation(3);
    public void SetInteract(bool interact) => canvasGroup.interactable = interact;
}