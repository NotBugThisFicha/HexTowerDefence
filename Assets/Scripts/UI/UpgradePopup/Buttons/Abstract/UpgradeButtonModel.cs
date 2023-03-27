using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class UpgradeButtonModel
{   
    private readonly UpgradeButtonOption option;
    private readonly CompositeDisposable disposable= new CompositeDisposable();
    private readonly GameManager gameManager;

    private IUpgradeButtonView upgradeButtonView;
    public float UpgradedProp => _upgradedValue;
    public UpgradeButtonModel(UpgradeButtonOption option, GameManager gameManager)
    {
        this.option = option;
        this.gameManager = gameManager;
        _upgradeMoneyValue = option.upgradeMoney;
        upgradeButtonView = new UpgradeButtonView(option.elements, option.textUpgrade);
    }

    private float _upgradedValue;
    private int _upgradeMoneyValue;

    private int counterClick;

    public void Init()
    {
        option.UpgradeButton.OnClickAsObservable().Subscribe(x =>
        {
            if(counterClick < option.elements.Length && gameManager.Money >= _upgradeMoneyValue)
            {
                SetUpgradeValue(option.upgradeValue);
                IncreaseUpgradeMoney(option.upgradeMoney);

                upgradeButtonView.ShowIncrementElement();
                upgradeButtonView.ChangeColorButton(Color.green, option.UpgradeButton);
            }
            else upgradeButtonView.ChangeColorButton(Color.red, option.UpgradeButton);

        }).AddTo(disposable);
    }

    public void Disable() =>  disposable.Clear();
    public void Destroy() => disposable.Dispose();

    protected virtual void SetUpgradeValue(float value) => _upgradedValue += value;

    private void IncreaseUpgradeMoney(int value)
    {
        _upgradeMoneyValue += value;
        upgradeButtonView.UpdateMoneyText(_upgradeMoneyValue);
        gameManager.DecriseCash(_upgradeMoneyValue);
    } 
}
