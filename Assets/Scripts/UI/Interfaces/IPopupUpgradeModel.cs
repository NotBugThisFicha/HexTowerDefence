using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IPopupUpgradeModel
{
    public float ChangeSpeedRX { get; }
    public float ChangeDamage { get; }
    public float ChangeDistanceRX { get; }
}
