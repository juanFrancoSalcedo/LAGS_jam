using AssetKits.ParticleImage;
using B_Extensions;
using UnityEngine;

public class UIParticlesService : Singleton<UIParticlesService>
{
    [SerializeField] private ParticleImage particleone;

    private void OnEnable() => MiningMediator.Subscribe(TypeMiningEvent.CollectResource, Collect);

    private void OnDisable() => MiningMediator.Unsubscribe(TypeMiningEvent.CollectResource, Collect);

    public void Collect() 
    {
        print("ParticlePool Particles Here");
        particleone.gameObject.SetActive(true);
    }
}
