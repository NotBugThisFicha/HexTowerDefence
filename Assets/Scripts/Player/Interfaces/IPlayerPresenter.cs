
using UniRx;
using UnityEngine;

public interface IPlayerPresenter
{
    public bool HasTarget { get; }
    public void SetTarget(Transform transform);
}
