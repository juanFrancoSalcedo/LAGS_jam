using UnityEngine;
using Zenject;

public class MiningInstaller : MonoInstaller
{
    [SerializeField] private RockView _rockView;
    [SerializeField] private int _rockMaxHP = 3;

    public override void InstallBindings()
    {
        var model = new RockModel(_rockMaxHP);
        var controller = new RockController(
            model,
            () => _rockView.PlayHitParticles(),
            () => _rockView.DestroyRock()
        );

        _rockView.Initialize(controller);

        Container.Bind<RockModel>().FromInstance(model).AsSingle();
        Container.Bind<RockController>().FromInstance(controller).AsSingle();
    }
}
