using UnityEngine;
using UnityEngine.UI;

public class RupeeUIController : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject watchAdButtonRoot;
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private Button watchAdButton;
    
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

        bool spentSuccessfully = currencyManager.TrySpendCurrency(5);

        if (spentSuccessfully)
        {
            Debug.Log("Tip jar spent 5 rupees.");
            saveManager?.Save();
        }
        else
        {
            Debug.Log("Tip jar failed. Not enough rupees.");
        }

        RefreshWatchAdVisibility();
    }

    private void RefreshWatchAdVisibility()
    {
        if (watchAdButtonRoot == null || currencyManager == null)
        {
            return;
        }

        bool shouldShowAdButton = currencyManager.currentCurrency < 5;

        watchAdButtonRoot.SetActive(shouldShowAdButton);

        if (shouldShowAdButton && watchAdButton != null)
        {
            watchAdButton.interactable = true;
        }
    }


}
