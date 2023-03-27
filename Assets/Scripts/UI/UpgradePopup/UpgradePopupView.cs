using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePopupView : IPopupUpgradeView
{
    private GameObject popup;
    public ReactiveCommand<bool> OnHidePopupCmd => OnHide;
    private ReactiveCommand<bool> OnHide = new ReactiveCommand<bool>();

    public void HidePopup()
    {
        popup.transform.DOLocalMoveX(1000, 0.3f).SetUpdate(true);
        OnHide.Execute(true);
    }

    public void SetPopup(GameObject popup) => this.popup = popup;

    public void ShowPopup()
    {
        popup.transform.DOLocalMoveX(-1000, 0.3f).SetUpdate(true);
        OnHide.Execute(false);
    }
}
