using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using TMPro;
using System.Collections;

public class PurchaseFulfillment : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject hideSinglePurchaseButton;
    public int availableRupees = 0;
    private const string RUPEE_1 = "Buy1Rupee";
    private const string RUPEE_10 = "Buy10Rupees";
    private const string WALLET_UPGRADE = "UpgradeWallet";

    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private float statusDuration =2f;
    private Coroutine clearStatusCoroutine;

    public void OnConfirmedOrder(ConfirmedOrder confirmedOrder)
    {
        var purchasedProductInfo = confirmedOrder.Info.PurchasedProductInfo;

        foreach(IPurchasedProductInfo info in purchasedProductInfo)
        {
            switch(info.productId)
            {
                case RUPEE_1:
                    Debug.Log($"You've added 1 Rupee to your Wallet!");
                    currencyManager.AddCurrency(1);
                    ShowStatus("Purchase Successful!");
                    break;
                case RUPEE_10:
                    Debug.Log($"You've added 10 Rupees to your Wallet!");
                    currencyManager.AddCurrency(10);
                    ShowStatus("Purchase Successful!");
                    break;
                case WALLET_UPGRADE:
                    Debug.Log("You've upgraded your wallet! You can now hold more Rupees!");
                    currencyManager.UpgradeWallet();
                    hideSinglePurchaseButton.SetActive(false);
                    ShowStatus("Purchase Successful!");
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
}
