using B_Extensions;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSound : BaseToggleAttendant 
{
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;
    AudioListener _audioListener; 

    private void Start()
    {
        _audioListener = FindFirstObjectByType<AudioListener>();
        toggleComponent.image.sprite = (toggleComponent.isOn) ? _spriteOn : _spriteOff;
        toggleComponent.onValueChanged.AddListener(Display);
    }

    private void Display(bool result)
    {
        toggleComponent.image.sprite = (result) ? _spriteOn : _spriteOff;
        _audioListener.enabled = result;
    }

    public void Configure(UnityAction<bool> events)
    {
        toggleComponent.onValueChanged.AddListener(events);
    }
}