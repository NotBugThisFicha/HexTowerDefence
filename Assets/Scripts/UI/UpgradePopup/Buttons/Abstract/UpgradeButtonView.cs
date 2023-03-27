using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public interface IUpgradeButtonView
{
    public void ShowIncrementElement();
    public void UpdateMoneyText(int value);
    public void ChangeColorButton(Color color, Button button);

}
public class UpgradeButtonView : IUpgradeButtonView
{
    private readonly GameObject[] elements;
    private readonly Text moneyText;
    public UpgradeButtonView(GameObject[] elements, Text text) {
        this.elements = elements;
        this.moneyText = text;
    }

    private int counterEl;

    public void ShowIncrementElement()
    {
        elements[counterEl].gameObject.SetActive(true);
        elements[counterEl].transform.DOScale(1, 0.3f).SetUpdate(true);
        counterEl++;
    }

    public void UpdateMoneyText(int value) => moneyText.text = $"${value}";

    public async void ChangeColorButton(Color color, Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        Color color1 = buttonImage.color;
        await buttonImage.DOColor(color, 0.3f).SetUpdate(true).AsyncWaitForCompletion();
        buttonImage.DOColor(color1, 0.3f).SetUpdate(true);
    }
}
