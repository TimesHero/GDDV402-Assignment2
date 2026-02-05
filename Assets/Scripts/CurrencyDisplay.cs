using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Refresh()
    {
        if (currencyManager == null || currencyText == null)
        {
            return;
        }

        currencyText.text = $"{currencyManager.currentCurrency.ToString()}/{currencyManager.walletCapacity.ToString()} Rupees";

    }

    private void Update()
    {
        Refresh();
    }
}
