using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MoveableSettingsInstaller", menuName = "Installers/MoveableSettingsInstaller")]
public class MoveableSettingsInstaller : ScriptableObjectInstaller<MoveableSettingsInstaller>
{
    [SerializeField] private Enemy.Settings enemySettings;
    [SerializeField] private Bullet.Settings bulletSettings;
    public override void InstallBindings()
    {
        Container.Bind<Enemy.Settings>().FromInstance(enemySettings).AsSingle();
        Container.Bind<Bullet.Settings>().FromInstance(bulletSettings).AsSingle();
    }
}