
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeSaveData 
{
    public List<UpgradeEntry> upgrades = new List<UpgradeEntry>();

}

[Serializable]
public class UpgradeEntry
{
    public string UpgradeName;
    public int upgradeCount;
    public int MaxUpgradeCount;
    public int FinishedUpgradeCount;
    public int MaxFinishedUpgradeCount;
}