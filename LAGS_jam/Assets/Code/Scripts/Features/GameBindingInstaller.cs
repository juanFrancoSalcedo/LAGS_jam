using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] private TraderHandler _traderHandler;
    public override void InstallBindings()
    {
        Container.BindInstance(DialogManager.Instance).AsSingle();
        Container.BindInstance(_traderHandler).AsTransient();
    }
}
