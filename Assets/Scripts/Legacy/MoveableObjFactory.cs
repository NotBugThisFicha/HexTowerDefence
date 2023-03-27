using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveableObjFactory : IFactory<Vector2, Moveable>
{
    private readonly PoolerNonMono _pooler;
    private readonly DiContainer _diContainer;
    public MoveableObjFactory(DiContainer diContainer, PoolerNonMono pooler)
    {
        _diContainer = diContainer;
        _pooler = pooler;
    }

    public Moveable Create(Vector2 pos)
    {
        //var prefab = _pooler.SpawnFromPool(Enum, pos, Quaternion.identity);
        Moveable moveable = null;
        //if(Enum == MoveableObjEnum.Bullet)
        //    moveable = _diContainer.Instantiate<Bullet>();
        //else moveable = _diContainer.Instantiate<Enemy>();

        return moveable;
    }
}
