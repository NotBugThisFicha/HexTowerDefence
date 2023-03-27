using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UISysInstaller : MonoInstaller<UISysInstaller>
{
    [SerializeField] private MainUIData uIData;
    [SerializeField] private UpgradePopupData popupData;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UISystem>().AsSingle().WithArguments(uIData);
        Container.BindInterfacesAndSelfTo<PopupUpgradeModel>().AsSingle().WithArguments(popupData);
        Container.BindInterfacesTo<UpgradePopupView>().AsSingle();
    }
}
