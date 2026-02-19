using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currentCurrency = 0;
    public int walletCapacity = 99;
    public bool walletUpgraded = false;
    private int maxWalletCapacity = 99;
    private int upgradedWalletCapacity = 999;

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

    public bool SpendCurrency(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (currentCurrency < amount)
        {
            return false;
        }

        currentCurrency -= amount;
        return true;
    }
}
