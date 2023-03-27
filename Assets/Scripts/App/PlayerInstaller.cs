using UnityEngine;
using Zenject;


[CreateAssetMenu(menuName ="Models/Player", fileName ="Player")]
public class PlayerInstaller : ScriptableObjectInstaller<PlayerInstaller>
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerModel.Settings playerModelSettings;
    public override void InstallBindings()
    {
        var pref = Instantiate(player);
        Container.BindInterfacesTo<PlayerView>().AsSingle().WithArguments(pref.transform);
        Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().WithArguments(playerModelSettings);
        Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle();
    }
}