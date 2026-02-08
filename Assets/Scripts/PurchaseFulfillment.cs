using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PurchaseFulfillment : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject walletUpgradeButton;

    [Header("Audio References")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] public AudioClip thankYou;
    [SerializeField] public AudioClip rupeeSound;
    [SerializeField] public AudioClip upgradeSound;
    
    public int availableRupees = 0;
    private const string RUPEE_1 = "Buy1Rupee";
    private const string RUPEE_10 = "Buy10Rupees";
    private const string WALLET_UPGRADE = "UpgradeWallet";
    
    [Header("On Screen Text")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private float statusDuration =2f;
    private Coroutine clearStatusCoroutine;


    private bool CanAddRupees(int amountToAdd)
    {
        int current = currencyManager.currentCurrency;
        int capacity = currencyManager.walletCapacity;

        return current + amountToAdd <= capacity;
    }
    public void OnConfirmedOrder(ConfirmedOrder confirmedOrder)
    {
        var purchasedProductInfo = confirmedOrder.Info.PurchasedProductInfo;

        foreach(IPurchasedProductInfo info in purchasedProductInfo)
        {
            switch(info.productId)
            {
                case RUPEE_1:
                    Debug.Log($"You've added 1 Rupee to your Wallet!");
                    if(!CanAddRupees(1))
                    {
                        ShowStatus("You can't carry any more rupees.");
                        return;
                    }
                        
                    currencyManager.AddCurrency(1);

                    //Audio things
                    if (sfxSource != null && thankYou != null)
                        sfxSource.PlayOneShot(thankYou, 1f);
                    if (sfxSource != null && rupeeSound != null)
                        StartCoroutine(PlaySoundWithDelay(rupeeSound, 1, 1f));

                    ShowStatus("Purchase Successful!");
                    saveManager.Save();
                    break;
                case RUPEE_10:
                    Debug.Log($"You've added 10 Rupees to your Wallet!");
                    if(!CanAddRupees(10))
                    {
                        ShowStatus("You can't carry any more rupees.");
                        return;
                    }
                    currencyManager.AddCurrency(10);

                    //Audio things
                    if (sfxSource != null && thankYou != null)
                        sfxSource.PlayOneShot(thankYou, 1f);
                    if (sfxSource != null && rupeeSound != null)
                        StartCoroutine(PlaySoundWithDelay(rupeeSound, 10, 0.1f));

                    ShowStatus("Purchase Successful!");
                    saveManager.Save();
                    break;

                case WALLET_UPGRADE:
                    Debug.Log("You've upgraded your wallet! You can now hold more Rupees!");
                    currencyManager.UpgradeWallet();
                    ShowStatus("Purchase Successful!");
                    saveManager.Save();
                    RemoveWalletUpgradeButton();
                    break;
            }
        }
    }

    public void OnFailedOrder(FailedOrder failedOrder)
    {
        var purchaseProductInfo = failedOrder.Info.PurchasedProductInfo;
        string items = string.Empty;

        foreach (IPurchasedProductInfo info in purchaseProductInfo)
        {
            items += ' ' + info.productId;
        }

        Debug.Log($"Failed to purchase the following items:{items}");
        Debug.Log($"Reason: '{failedOrder.FailureReason}', Details: '{failedOrder.Details}'");
        ShowStatus("Purchase Failed! Please Try Again Later!");
    }

    private void GrantRupee(int rupeeAmount)
    {
        availableRupees += rupeeAmount;
        Debug.Log($"You Purchased {rupeeAmount} Rupee(s)!");
    }

    private void ShowStatus(string message)
    {
        if (statusText == null)
        return;

        statusText.text = message;

        if(clearStatusCoroutine != null)
        StopCoroutine(clearStatusCoroutine);

        clearStatusCoroutine = StartCoroutine(ClearStatusAfterDelay());
    }

    private IEnumerator ClearStatusAfterDelay()
    {
        yield return new WaitForSeconds(statusDuration);

        if (statusText != null)
        statusText.text = string.Empty;

        clearStatusCoroutine = null;
    }


    private void RemoveWalletUpgradeButton()
    {
        if (walletUpgradeButton == null || currencyManager == null)
            return;

        walletUpgradeButton.SetActive(!currencyManager.walletUpgraded);

    }
    
    IEnumerator PlaySoundWithDelay(AudioClip clip, int times, float delay) 
    {
        if (sfxSource == null || clip == null || times <= 0)
        yield break;
        
     for (int i = 0; i < times; i++)
        {
            sfxSource.PlayOneShot(clip);
            yield return new WaitForSeconds(delay);
        }
        
    }

    private void Start()
    {
        RemoveWalletUpgradeButton();
    }

}
