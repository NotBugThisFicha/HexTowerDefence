using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerPresenter : IInitializable, IDisposable, IPlayerPresenter
{
    private readonly IPlayerView _playerView;
    private readonly IPlayerModel _playerModel;
    private readonly VFXSystem _VFXSys;

    private Transform _target;

    private CompositeDisposable disposable = new CompositeDisposable();
    private Transform playerTransform;

    public bool HasTarget { 
        get {
            if (_target == null)
                return false;
            else return true;
        } 
    }

    public PlayerPresenter(
        IPlayerModel playerModel, 
        IPlayerView playerView, 
        VFXSystem vfxSys){
        
        _VFXSys= vfxSys;
        _playerModel = playerModel;
        _playerView = playerView;
    }

    public void Initialize()
    {

        playerTransform = _playerView.GetTransform();
        _VFXSys.AddRenderer(playerTransform.GetChild(0).GetComponent<SpriteRenderer>());
        _VFXSys.AddRenderer(playerTransform.GetChild(1).GetComponent<SpriteRenderer>());
        Quaternion quaternion;
        bool oneStartCorot = true;

        Observable.EveryUpdate().Subscribe(_ => {
            if (_target != null && _target.gameObject.activeInHierarchy)
            {
                _playerView.Rotate(RotationProcces(playerTransform, out quaternion));
                if (oneStartCorot)
                {
                    MainThreadDispatcher.StartCoroutine(LongShoot());
                    oneStartCorot = false;
                }
            }
            else _target = null;
        }).AddTo(disposable);
    }

    private IEnumerator LongShoot()
    {
        while(true)
        {
            if (_target != null)
            {
               
                _playerView.Shoot(_target.position, _playerModel.DamageBullet);
                yield return Observable.Timer(TimeSpan.FromSeconds(_playerModel.ShootDelay)).ToYieldInstruction();
            }
            else yield return null;
        }
    }

    public void Dispose() => disposable.Dispose();
    public Quaternion RotationProcces(Transform playerTr, out Quaternion quaternion)
    {
        var direction = playerTr.position - _target.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        quaternion = Quaternion.Euler(0, 0, angle);
        return Quaternion.Lerp(playerTr.rotation, quaternion, Time.deltaTime * _playerModel.Speed.Value);
    }

    public void SetTarget(Transform transform) => _target = transform;
}
