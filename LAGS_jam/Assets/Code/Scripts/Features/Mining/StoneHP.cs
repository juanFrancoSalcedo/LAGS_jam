using System;

public class StoneHP
{
    public int CurrentHP { get; private set; }
    public int MaxHP { get; }
    public event Action<int> OnHPChanged;
    public event Action OnDestroyed;

    public StoneHP(int maxHP)
    {
        MaxHP = maxHP;
        CurrentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHP <= 0) return;

        CurrentHP -= damage;
        OnHPChanged?.Invoke(CurrentHP);

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            OnDestroyed?.Invoke();
        }
    }

    public bool IsAlive => CurrentHP > 0;
}
