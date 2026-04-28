using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [SerializeField] CurrencyManager currencyManager;
    [SerializeField] PurchaseFulfillment purchaseFulfillment;

    private string saveFileName = "RupeeSave.json";
    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    private void Awake()
    {
        Load();
    }


    public void Save()
    {
        if (currencyManager == null)
        return;

        SaveData data = new SaveData();
        data.currentCurrency = currencyManager.currentCurrency;
        data.walletCapacity = currencyManager.walletCapacity;
        data.walletUpgraded = currencyManager.walletUpgraded;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public void Load()
    {
        if (currencyManager == null)
        {
            return;
        }

        if (!File.Exists(SavePath))
        {
            ApplyDefaults();
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        currencyManager.currentCurrency = data.currentCurrency;
        currencyManager.walletCapacity = data.walletCapacity;
        currencyManager.walletUpgraded = data.walletUpgraded;
    }

    public void ApplyDefaults()
    {
        if (currencyManager == null)
        {
            return;
        }

        currencyManager.currentCurrency = 0;
        currencyManager.walletCapacity = 99;
        currencyManager.walletUpgraded = false;

        Save();
    }

    public void ClearSaveFile()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }

        ApplyDefaults();

        Debug.Log("Save file cleared and defaults applied.");
    }

}
