using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject ShopMainObject;
    public GameObject[] MenuObject;
    public Button[] UpgradeButtons;
    public int MenuIndex = 0;

    public List<UpgradeEntry> allUpgrades;

    private Dictionary<string, int> playerUpgrades = new Dictionary<string, int>();
    private string savePath;
    private const string SAVE_KEY = "UpgradeData";
    public void Start()
    {
        ChangeMenu(0);
    }
    
    public bool BuyUpgrade(string upgradeName)
    {
        if (!CanUpgrade(upgradeName)) return false;

        playerUpgrades[upgradeName]++;
        SaveUpgrades();
        Debug.Log($"Upgraded {upgradeName} to level {playerUpgrades[upgradeName]}");
        return true;
    }

    public bool CanUpgrade(string upgradeName)
    {
        var upgrade = allUpgrades.Find(u => u.name == upgradeName);
        if (upgrade == null) return false;

        return playerUpgrades[upgradeName] < upgrade.maxLevel;
    }

    public int GetUpgradeLevel(string upgradeName)
    {
        return playerUpgrades.ContainsKey(upgradeName) ? playerUpgrades[upgradeName] : 0;
    }
    public void SaveUpgrades()
    {
        UpgradeSaveData saveData = new UpgradeSaveData();

        foreach (var kvp in playerUpgrades)
        {
            saveData.upgrades.Add(new UpgradeEntry { name = kvp.Key, upgradeCount = kvp.Value });
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("Upgrades saved to PlayerPrefs.");
    }
    public void LoadUpgrades()
    {
        
        foreach (var upgrade in allUpgrades)
        {
            playerUpgrades[upgrade.name] = 0;
        }

        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(json);

            foreach (var entry in saveData.upgrades)
            {
                if (playerUpgrades.ContainsKey(entry.name))
                {
                    playerUpgrades[entry.name] = entry.upgradeCount;
                }
            }

            Debug.Log("Upgrades loaded from PlayerPrefs.");
        }
        else
        {
            Debug.Log("No saved upgrade data found.");
        }
    }
    public void ToggleShopMenu()
    {
        ShopMainObject.SetActive(!ShopMainObject.activeInHierarchy);
    }
    public void ChangeMenu(int i)
    {
        MenuObject[MenuIndex].SetActive(false);
        MenuObject[i].SetActive(true);
        MenuIndex = i;
    }
}
