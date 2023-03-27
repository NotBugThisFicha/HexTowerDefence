using Zenject;

public class CollisionSysInstaller : MonoInstaller<CollisionSysInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CollisionSystem>().AsSingle();
    }
}
