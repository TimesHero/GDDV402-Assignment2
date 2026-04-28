using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currentCurrency = 0;
    public int walletCapacity = 99;
    public bool walletUpgraded = false;
    private int maxWalletCapacity = 99;
    private int upgradedWalletCapacity = 999;

    private int totalRupeesSpentSinceLastAd = 0;

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;

        if (currentCurrency > walletCapacity)
        {
            currentCurrency = walletCapacity;
        }
    }

    public void UpgradeWallet()
    {
        walletUpgraded = true;
        walletCapacity = upgradedWalletCapacity;
        maxWalletCapacity = upgradedWalletCapacity;
    }

    public bool TrySpendCurrency(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (currentCurrency <= 0)
        {
            return false;
        }

        int amountToSpend = Mathf.Min(amount, currentCurrency);

        currentCurrency -= amountToSpend;
        totalRupeesSpentSinceLastAd += amountToSpend;

        Debug.Log($"Tip jar spent {amountToSpend} rupees.");

        return true;
    }

    public bool HasReachedInterstitialThreshold(int threshold)
    {
        return totalRupeesSpentSinceLastAd >= threshold;
    }

    public void ResetInterstitialCounter()
    {
        totalRupeesSpentSinceLastAd = 0;
    }
}
