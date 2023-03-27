using System;
using UniRx;
using Zenject;

public class PopupUpgradeModel : IInitializable, IDisposable, IPopupUpgradeModel
{
    private readonly UpgradeButtonOption[] upgradeButtonOptions;
    private readonly GameManager gameManager;
    private readonly IPopupUpgradeView _view;
    private readonly CompositeDisposable disposables = new CompositeDisposable();

    private UpgradeButtonModel[] upgradeButtons;

    public PopupUpgradeModel(UpgradePopupData upgradePopupData, IPopupUpgradeView popupUpgradeView, GameManager gameManager)
    {
        this.gameManager= gameManager;
        upgradeButtonOptions = upgradePopupData.upgradeButtonOptions.ToArray();
        _view = popupUpgradeView;
        _view.SetPopup(upgradePopupData.popup);
    }

    public float ChangeDistanceRX => upgradeButtons[0].UpgradedProp;
    public float ChangeSpeedRX => upgradeButtons[1].UpgradedProp;

    public float ChangeDamage => upgradeButtons[2].UpgradedProp;

    public void Initialize()
    {
        upgradeButtons = new UpgradeButtonModel[]
        {
            new UpgradeButtonDistance(upgradeButtonOptions[0], gameManager),
            new UpgradeButtonSpeed(upgradeButtonOptions[1], gameManager),
            new UpgradeButtonDamage(upgradeButtonOptions[2], gameManager),
        };


        _view.OnHidePopupCmd.Subscribe(x =>
        {
            if (!x)
                foreach (var upgradeButton in upgradeButtons)
                    upgradeButton.Init();
            else
                foreach (var upgradeButton in upgradeButtons)
                    upgradeButton.Disable();
        }).AddTo(disposables);
    }
    public void Dispose()
    {
        foreach (UpgradeButtonModel upgradeButtonModel in upgradeButtons)
            upgradeButtonModel.Destroy();
        disposables.Dispose();
    }
}
