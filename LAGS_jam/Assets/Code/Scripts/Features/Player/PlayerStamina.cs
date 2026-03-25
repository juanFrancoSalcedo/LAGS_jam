using System;
using UnityEngine;

public class PlayerStamina
{
    public float Stamina { get; private set; }
    private float maxStamina;
    public PlayerStamina()
    {
        maxStamina = Stamina = 80;
        Debug.Log("Shutarse esto");
    }

    public void DebtStamina(float amount) 
    {
        Stamina -= amount;
        OnStaminaDrain?.Invoke(Stamina, maxStamina);
    }

    public static event Action<float,float> OnStaminaDrain;
}     