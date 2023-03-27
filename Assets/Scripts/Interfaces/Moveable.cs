using ModestTree;
using System;
using UniRx;
using UniRx.Triggers;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class MoveableSettings
{
    public float speed;
    public float HP;
    public float damage;
}
public abstract class Moveable: IDisposable, IPoolerNativ, ICollision
{
    private readonly MoveableSettings settings;
    private readonly CollisionSystem collisionSystem;
    protected readonly VFXSystem VFXSystem;

    private float _health;
    private float _damage;

    public Moveable(MoveableSettings settings, CollisionSystem collisionSystem, VFXSystem vfxSystem)
    {
        this.settings = settings;
        this.collisionSystem = collisionSystem;
        this.VFXSystem = vfxSystem;
    }

    private Vector3 directionMove;
    private Collider2D _myCollider;
    protected Transform _myTransfrom;
    private bool isInitRender;
    private bool IsDead => _health <= 0;
    float ICollision.DamageValue { get => _damage; }

    protected readonly CompositeDisposable disposable = new CompositeDisposable();

    #region SystemMethod
    public void Enable() => OnEnable();
    protected void OnEnable()
    {
        Assert.IsNotNull(_myTransfrom);
        if (!isInitRender)
            VFXSystem.AddRenderer(_myTransfrom.GetComponent<SpriteRenderer>());

        _damage = settings.damage;
        _health = settings.HP;
        _myTransfrom.gameObject.SetActive(true);

        bool isVisible = false;
        _myTransfrom.OnBecameInvisibleAsObservable()
            .Subscribe(_ => isVisible = true)
            .AddTo(disposable);

        Observable.EveryUpdate().Subscribe(_ =>{
            OnUpdate(isVisible);
        }).AddTo(disposable);

        OnCollisionEnterObserv();
    }
    protected virtual void OnUpdate(bool isVisible)
    {
        if (directionMove != Vector3.zero)
            _myTransfrom.Translate(directionMove * settings.speed * Time.deltaTime);
        else throw new InvalidOperationException($"{directionMove} should be non zero");

        if (IsDead || isVisible)
        {
            Disable();
            if (IsDead)
                OnDeath();
        }
    }
    public void Disable()
    {
        _myTransfrom.gameObject.SetActive(false);
        disposable.Clear();
    }
    public void Dispose() => disposable.Dispose();
    protected virtual void OnDeath()
    {

    }

    public void TakeDamage(float value) => _health -= value;
    public void SetDamage(float damage) => _damage = damage;
    #endregion

    #region CollisionInteractMethod
    private void OnCollisionEnterObserv()
    {
        if (_myCollider)
        {
            _myCollider.OnCollisionEnter2DAsObservable()
                .Where(x => x.gameObject.layer != LayerMask.NameToLayer("NotTriggered"))
                .Subscribe(x => { OnCollisionAction(); })
                .AddTo(disposable);
        }
    }
    private void OnCollisionAction() => collisionSystem.AddCollision(this);
    #endregion

    public void SetTransform(Transform transform)
    {
        _myTransfrom= transform;
        SetMyCollider(transform);
    }

    public void SetStartPosition(Vector3 pos) => _myTransfrom.position = pos;
    public void SetStartRotation(Quaternion quaternion) => _myTransfrom.rotation = quaternion;

    private void SetMyCollider(Transform transform) => 
        _myCollider = transform.GetComponent<Collider2D>();
    public void SetDirectionTo(Vector3 target) => 
        directionMove = (target - _myTransfrom.position);
}
