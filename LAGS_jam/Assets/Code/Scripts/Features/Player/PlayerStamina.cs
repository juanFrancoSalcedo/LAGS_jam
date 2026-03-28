using System;
using UnityEngine;

public class PlayerStamina
{
    public float Stamina { get; private set; } = 0;
    private float maxStamina =0;
    public PlayerStamina()
    {
        if(maxStamina != 80)
            maxStamina = Stamina = 80;
    }

    public void DebtStamina(float amount) 
    {
        Stamina -= amount;
        OnStaminaDrain?.Invoke(Stamina, maxStamina);
    }

    public static event Action<float,float> OnStaminaDrain;
}     