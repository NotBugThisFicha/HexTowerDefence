using System;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerModel : IPlayerModel
{
    private readonly Settings _settings;
    private readonly IPopupUpgradeModel _upgradeModel;
    public PlayerModel(Settings settings, IPopupUpgradeModel upgradeModel)
    {
        _settings = settings;
        _upgradeModel = upgradeModel;
    }

    public IReadOnlyReactiveProperty<float> CurrentHP => new ReactiveProperty<float>(_settings.health);

    public IReadOnlyReactiveProperty<bool> IsDead => CurrentHP.Select(x => x <= 0).ToReactiveProperty();

    public IReadOnlyReactiveProperty<float> Speed => new ReactiveProperty<float>(_settings.rotSpeed);
    public float DamageBullet => _settings.damageBullet + _upgradeModel.ChangeDamage;

    public float Radius => _settings.radius + _upgradeModel.ChangeDistanceRX;

    public float ShootDelay => _settings.shootDelay + _upgradeModel.ChangeSpeedRX;

    public void TakeDamage(float value)
    {
        _settings.health -= value;
    }

    [Serializable]
    public class Settings
    {
        public float rotSpeed;
        public float health;
        public float radius;
        public float shootDelay;
        public float damageBullet;
    }
}
