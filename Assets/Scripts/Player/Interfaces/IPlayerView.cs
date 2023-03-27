
using UnityEngine;

public interface IPlayerView
{
    public Transform GetTransform();
    public void Rotate(Quaternion quaternion);
    public void Shoot(Vector3 direction);
    public void Shoot(Vector3 target, float withDamage);
}
