using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PoolersInstaller : MonoInstaller<PoolersInstaller>
{
    [SerializeField] private List<PoolNonMono> listPools;
    [SerializeField] private List<PoolerGO.Pool> listPoolsGO;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PoolerNonMono>().AsSingle().WithArguments(listPools);
        Container.BindInterfacesAndSelfTo<PoolerGO>().AsSingle().WithArguments(listPoolsGO);
    }
}
