using System;
using UniRx;
using UnityEngine;

[PoolMember]
public class Enemy : Moveable
{
    private readonly IPlayerModel _playerModel;
    private readonly IPlayerPresenter playerPresenter;
    private readonly GameManager gameManager;

    public Enemy( 
        IPlayerPresenter playerPresenter, 
        IPlayerModel playerModel, 
        Settings settings,
        CollisionSystem system,
        VFXSystem VFXSystem,
        GameManager gameManager) : base(settings, system, VFXSystem) { 

        _playerModel = playerModel;
        this.playerPresenter = playerPresenter;
        this.gameManager = gameManager;
    }
    protected override void OnUpdate(bool isVisible)
    {
        if (!playerPresenter.HasTarget && Vector2.Distance(Vector2.zero, _myTransfrom.position) <= _playerModel.Radius)
            playerPresenter.SetTarget(_myTransfrom);
        base.OnUpdate(isVisible);
    }
    protected override void OnDeath() {
        
        gameManager.UpdateMoney();
        VFXSystem.CreateVFX("EnemyVFX", _myTransfrom.position, _myTransfrom.rotation);
    } 

    [Serializable]
    public class Settings: MoveableSettings
    {

    }
    
}
