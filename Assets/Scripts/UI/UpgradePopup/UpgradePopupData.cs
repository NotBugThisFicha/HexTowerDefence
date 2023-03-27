using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UpgradeButtonOption
{
    public Button UpgradeButton;
    public GameObject[] elements = new GameObject[3];
    public Text textUpgrade;
    public int upgradeMoney;
    public float upgradeValue;
}

[Serializable]
public class UpgradePopupData
{
    public GameObject popup;
    public List<UpgradeButtonOption> upgradeButtonOptions;
}
