using UnityEngine;

public class RupeeUIController : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject watchAdButtonRoot;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshWatchAdVisibility();
    }

    public void spendRupeesWhenClicked()
    {
        if (currencyManager == null)
        {
            return;
        }

        currencyManager.TrySpendCurrency(5);
        RefreshWatchAdVisibility();
    }

    private void RefreshWatchAdVisibility()
    {
        if (watchAdButtonRoot == null || currencyManager == null)
        {
            return;
        }

        watchAdButtonRoot.SetActive(currencyManager.currentCurrency == 0);
    }


}
