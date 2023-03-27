using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerView : IPlayerView
{
    private readonly Transform transform;
    private readonly Collider2D _myCollider;
    private readonly MoveableSpawner spawner;
    public PlayerView(Transform transform, MoveableSpawner spawner)
    {
        this.transform = transform;
        this.spawner = spawner;
        _myCollider = this.transform.GetComponent<Collider2D>();
    }
    public Transform GetTransform() => transform;

    public void Rotate(Quaternion quaternion)
    {
        transform.rotation= quaternion;
    }


    public void Shoot(Vector3 target)
    {
        spawner.CreateMoveable<Bullet>(transform.position)
            .SetDirectionTo((target - transform.position).normalized);
    }

    public void Shoot(Vector3 target, float withDamage)
    {
        Moveable moveable = spawner.CreateMoveable<Bullet>(transform.position);
        moveable.SetDirectionTo((target - transform.position).normalized);
        moveable.SetDamage(withDamage);
    }
}
