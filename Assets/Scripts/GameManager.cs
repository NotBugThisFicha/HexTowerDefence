
using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class GameManager: IInitializable, IDisposable
{
    private const int moneyUpdateValue = 1;

    private readonly VFXSystem dessolveMaterialSys;
    private readonly UISystem uISystem;
    private readonly CompositeDisposable disposable = new CompositeDisposable();

    private int moneyValue;
    public int Money => moneyValue;

    public GameManager(VFXSystem dessolveMaterialSys, UISystem uISystem)
    {
        this.dessolveMaterialSys = dessolveMaterialSys;
        this.uISystem= uISystem;
    }

    public void Initialize()
    {
        uISystem.ExecuteClickButton.Subscribe(b =>
        {
            if (b)
                PauseGame();
            else ResumeGame();
        }).AddTo(disposable);
    }

    public void Dispose() => disposable.Dispose();

    public void PauseGame()
    {
        dessolveMaterialSys.DessolveAllMaterial();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        dessolveMaterialSys.ReverseAllMaterial();
        Time.timeScale = 1;
    }

    public void UpdateMoney()
    {
        moneyValue += moneyUpdateValue;
        uISystem.UpdateTextMoney(moneyValue);
    }

    public void DecriseCash(int value)
    {
        moneyValue -= value;
        uISystem.UpdateTextMoney(moneyValue);
    }
}
