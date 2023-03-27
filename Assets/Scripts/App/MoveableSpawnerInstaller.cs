using UnityEngine;
using Zenject;

public class MoveableSpawnerInstaller : MonoInstaller
{
    [SerializeField] private MoveableSpawner.Settings settings;
    public override void InstallBindings()
    {
        Container.Bind<MoveableSpawner.Settings>().FromInstance(settings).AsSingle();
        Container.BindInterfacesAndSelfTo<MoveableSpawner>().AsSingle();
    }
}