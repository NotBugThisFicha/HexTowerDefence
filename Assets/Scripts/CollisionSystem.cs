using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class CollisionSystem : IInitializable, IDisposable
{
    private ReactiveCollection<ICollision> _collisions= new ReactiveCollection<ICollision>();
    private readonly CompositeDisposable _disposable = new CompositeDisposable();
    public void Initialize()
    {
        _collisions.ObserveAdd().Subscribe(x =>{
            
            if(_collisions.Count >= 2){
                _collisions[0].TakeDamage(_collisions[1].DamageValue);
                _collisions[1].TakeDamage(_collisions[0].DamageValue);
                _collisions.Clear();
            }
        }).AddTo(_disposable);
    }

    public void AddCollision(ICollision collision) => 
        _collisions.Add(collision);
    public void Dispose()
    {
        _disposable.Clear();
        _disposable.Dispose();
    }
}
