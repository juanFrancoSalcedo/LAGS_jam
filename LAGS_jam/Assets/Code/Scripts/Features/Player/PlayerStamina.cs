using System;
using UnityEngine;

public class PlayerStamina
{
    public float Stamina { get; private set; } = 0;
    private float maxStamina =0;
    public static event Action<float,float> OnStaminaChanged;
    public PlayerStamina()
    {
        if(maxStamina != 80)
            maxStamina = Stamina = 80;
    }

    public void DebtStamina(float amount) 
    {
        Stamina -= amount;
        OnStaminaChanged?.Invoke(Stamina, maxStamina);
    }

    public void RestoreStamina(float amount)
    {
        Stamina += amount;
        if (Stamina > maxStamina)
            Stamina = maxStamina;
        OnStaminaChanged?.Invoke(Stamina, maxStamina);
    }
}     