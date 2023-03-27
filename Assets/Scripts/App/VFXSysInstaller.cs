using UnityEngine;
using Zenject;

public class VFXSysInstaller : MonoInstaller
{
    [SerializeField] private float tomeToDissolve;
    public override void InstallBindings()
    {
        Container.Bind<VFXSystem>().AsSingle().WithArguments(tomeToDissolve);
    }
}