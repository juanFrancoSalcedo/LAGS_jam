using System;
using UnityEngine;

public class RockController
{
    private RockModel _model;
    private Action _onHitParticles;
    private Action _onDestroyed;

    public RockController(RockModel model, Action onHitParticles, Action onDestroyed)
    {
        _model = model;
        _onHitParticles = onHitParticles;
        _onDestroyed = onDestroyed;

        _model.Health.OnHPChanged += HandleHPChanged;
        _model.Health.OnDestroyed += HandleDestroyed;
    }

    public void TakeHit(int damage)
    {
        if (!_model.Health.IsAlive) return;

        _model.Health.TakeDamage(damage);
    }

    private void HandleHPChanged(int currentHP)
    {
        _onHitParticles?.Invoke();
    }

    private void HandleDestroyed()
    {
        SpawnEmerald();
        _onDestroyed?.Invoke();
    }

    private void SpawnEmerald()
    {
        // TODO: Implement emerald spawn
        // _emeraldSpawner.Spawn(transform.position);
    }

    public void StoreInInventory()
    {
        // TODO: Implement inventory storage
        // _inventoryService.AddItem("Emerald", _model.DropAmount);
    }
}
