using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject ShopMainObject;
    public GameObject[] MenuObject;
    public int MenuIndex = 0;

    public List<UpgradeSaveData> allUpgrades; 
    private Dictionary<string, UpgradeEntry> playerUpgrades = new Dictionary<string, UpgradeEntry>();
    private const string SAVE_KEY = "NewNewUpgradeData";

    void Awake()
    {
        LoadUpgrades();
    }

    public bool CanUpgrade(string upgradeName)
    {
        if (!playerUpgrades.ContainsKey(upgradeName))
            return false;

        return playerUpgrades[upgradeName].upgradeCount < playerUpgrades[upgradeName].MaxUpgradeCount;
    }

    public void ApplyUpgrade(string upgradeName)
    {
        if (!CanUpgrade(upgradeName)) return;

        playerUpgrades[upgradeName].upgradeCount++;
        SaveUpgrades();
        Debug.Log($"Upgraded {upgradeName} to level {playerUpgrades[upgradeName].upgradeCount}");
        
    }

    public int GetUpgradeLevel(string upgradeName)
    {
        return playerUpgrades.ContainsKey(upgradeName) ? playerUpgrades[upgradeName].upgradeCount : 0;
    }

    public void SaveUpgrades()
    {
        UpgradeSaveData saveData = new UpgradeSaveData();

        foreach (var kvp in playerUpgrades)
        {
            saveData.upgrades.Add(kvp.Value);
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("Upgrades saved to PlayerPrefs.");
    }

    public void LoadUpgrades()
    {
        playerUpgrades.Clear();

        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(json);

            if (saveData != null && saveData.upgrades != null)
            {
                foreach (var entry in saveData.upgrades)
                {
                    playerUpgrades[entry.UpgradeName] = entry;
                    Debug.Log($"Loaded: {entry.UpgradeName} = {entry.upgradeCount}");
                }
            }
            else
            {
                Debug.LogWarning("Invalid or empty upgrade save data.");
            }
        }
        else
        {
            Debug.Log("No saved upgrade data found. Creating fresh entries.");
            
        }
    }

    public void ResetUpgrades()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        LoadUpgrades();
    }

    public UpgradeEntry GetUpgradeData(string upgradeName)
    {
        if (playerUpgrades.TryGetValue(upgradeName, out var data))
            return data;

        return null;
    }
    public void ToggleShopMenu()
    {
        ShopMainObject.SetActive(!ShopMainObject.activeInHierarchy);
        if (ShopMainObject.activeInHierarchy)
        {
            LoadUpgrades();
        }
    }
    public void ChangeMenu(int i)
    {
        MenuObject[MenuIndex].SetActive(false);
        MenuObject[i].SetActive(true);
        MenuIndex = i;
    }
}
