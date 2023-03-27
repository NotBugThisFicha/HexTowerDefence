
using UniRx;

public interface IPlayerModel
{
    public IReadOnlyReactiveProperty<float> CurrentHP { get; }
    public IReadOnlyReactiveProperty<float> Speed { get; }
    public IReadOnlyReactiveProperty<bool> IsDead { get; }
    public float DamageBullet { get; }
    public float Radius { get;}
    public float ShootDelay { get; }
    public void TakeDamage(float value);
}
