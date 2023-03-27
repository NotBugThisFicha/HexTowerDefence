using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class UISystem: IInitializable, IDisposable
{
    private readonly MainUIData _uiData;
    private readonly CompositeDisposable _disposable = new CompositeDisposable();
    private readonly IPopupUpgradeView upgradeView;

    public readonly ReactiveCommand<bool> ExecuteClickButton= new ReactiveCommand<bool>();

    private int countClick;
    public UISystem(MainUIData uiData, IPopupUpgradeView popupUpgradeView)
    {
        _uiData = uiData;
        upgradeView = popupUpgradeView;
    }

    public void Dispose() => _disposable.Dispose();

    public void Initialize()
    {
        _uiData.buttonUpgrade.OnClickAsObservable().Subscribe(_ =>
        {
            if(countClick == 0)
            {
                upgradeView.ShowPopup();
                ExecuteClickButton.Execute(true);
                countClick++;
            }
            else if(countClick == 1)
            {
                upgradeView.HidePopup();
                ExecuteClickButton.Execute(false);
                countClick = 0;
            }
        }).AddTo(_disposable);
    }

    public void UpdateTextMoney(int value) => _uiData.textMoney.text = $"${value}";

}
[Serializable]
public class MainUIData
{
    public Button buttonUpgrade;
    public Text textMoney;
}
