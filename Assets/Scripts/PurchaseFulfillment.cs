using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PurchaseFulfillment : MonoBehaviour
{
    public int availableGems = 0;
    private const string GEMS_5 = "Buy5Jems";
    private const string GEMS_10 = "Buy10Jems";

    public void OnConfirmedOrder(ConfirmedOrder confirmedOrder)
    {
        var purchasedProductInfo = confirmedOrder.Info.PurchasedProductInfo;

        foreach(IPurchasedProductInfo info in purchasedProductInfo)
        {
            switch(info.productId)
            {
                case GEMS_5:
                    GrantGems(5);
                    break;
                case GEMS_10:
                    GrantGems(10);
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
    }

    private void GrantGems(int gemAmount)
    {
        availableGems += gemAmount;
        Debug.Log($"You Purchased {gemAmount} gems!");
    }
}
