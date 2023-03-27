using System;
using UniRx;
using UnityEngine;


[PoolMember]
public class Bullet : Moveable
{
    public Bullet(Settings settings, 
        CollisionSystem system, 
        VFXSystem dessolveMaterialSys) 

        : base(settings, system, dessolveMaterialSys)
    {
   
    }


    [Serializable]
    public class Settings : MoveableSettings
    {

    }
}
