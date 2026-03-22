using UnityEngine;
using Zenject;

public class MiningInstaller : MonoInstaller
{
    [SerializeField] private RockHandler _rockView;
    [SerializeField] private int _rockMaxHP = 3;

    public override void InstallBindings()
    {

    }
}
