using UniRx;
using UnityEngine;
using UnityEngine.UI;

public interface IPopupUpgradeView
{
    public void ShowPopup();
    public void HidePopup();
    public void SetPopup(GameObject popup);
    public ReactiveCommand<bool> OnHidePopupCmd { get; }
}
