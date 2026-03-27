using AssetKits.ParticleImage;
using B_Extensions;
using System;
using UnityEngine;

public class UIParticlesService : Singleton<UIParticlesService>
{
    [SerializeField] private ParticleImage particleGems;
    [SerializeField] private ParticleImage particleMoney;

    private void OnEnable()
    {
        MiningMediator.Subscribe(TypeMiningEvent.CollectResource, Collect);
        MoneyDataService.OnMoneyChanged += CollectMoney;
    }


    private void OnDisable()
    {
        MiningMediator.Unsubscribe(TypeMiningEvent.CollectResource, Collect);
        MoneyDataService.OnMoneyChanged -= CollectMoney;
    }
    private void CollectMoney(int arg1, int arg2)
    {
        particleMoney.gameObject.SetActive(true);
    }

    public void Collect() 
    {
        particleGems.gameObject.SetActive(true);
    }
}
