using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller<GameManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
    }
}
