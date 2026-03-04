using System.Runtime.CompilerServices;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private string rewardedPlacementPrefix;

    [SerializeField] private string interstitialPlacementPrefix;
    [SerializeField] private int interstitialThreshold = 50;
    [SerializeField] private string bannerPlacementPrefix;


    public Button showAdButton;
    private string adUnitAffix;
    private string bannerPlacementId;

    private void Awake()
    {
#if UNITY_IOS
        adUnitAffix = "_iOS";
#elif UNITY_ANDROID
        adUnitAffix = "_Android";
#elif UNITY_EDITOR
        adUnitAffix = "_Android";
#endif
        //showAdButton.interactable = false;
        //LoadAd("RewardedRupees");
        bannerPlacementId = bannerPlacementPrefix + adUnitAffix;
    }

    private void Start()
    {
        LoadAd(rewardedPlacementPrefix);
        LoadAd(interstitialPlacementPrefix);

        LoadBanner();
        ShowBanner();
    }

    private void Update()
    {
        if (currencyManager == null)
        {
            return;
        }

        if (currencyManager.HasReachedInterstitialThreshold(interstitialThreshold))
        {
            ShowAd(interstitialPlacementPrefix);
            currencyManager.ResetInterstitialCounter();
            LoadAd(interstitialPlacementPrefix);
        }
    }

    public void LoadAd(string adUnitPrefix)
    {
        string adUnitID = adUnitPrefix + adUnitAffix;
        Advertisement.Load(adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"{placementId} loaded successfully");
        showAdButton.interactable = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning($"{placementId} failed to load: {error} - {message}");
    }

    public void ShowAd(string adUnitPrefix)
    {
        Debug.Log($"Trying to show rewarded ad. Using placement: {adUnitPrefix + adUnitAffix}");
        string adUnitID = adUnitPrefix + adUnitAffix;
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning($"{placementId} failed to show: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"{placementId} started.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"{placementId} clicked.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad finished. Placement: {placementId}, Completion State: {showCompletionState}");

        if (showCompletionState != UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Ad was not fully watched. No rupees will be rewarded.");
            return;
        }

        string rewardedPlacementId = rewardedPlacementPrefix + adUnitAffix;

        if (placementId == rewardedPlacementId)
        {
            if (currencyManager != null)
            {
                Debug.Log("Rewarded ad completed successfully. Adding 10 rupees to the wallet.");
                currencyManager.AddCurrency(10);
                LoadAd(rewardedPlacementPrefix);
            }
            else
            {
                Debug.LogWarning("CurrencyManager reference is missing. Cannot reward rupees.");
            }
        }
    }

    public void LoadBanner()
    {
        if (string.IsNullOrEmpty(bannerPlacementId))
            return;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        Advertisement.Banner.Load(bannerPlacementId, new BannerLoadOptions
        {
            loadCallback = () => Debug.Log($"Banner loaded: {bannerPlacementId}"),
            errorCallback = (message) => Debug.LogWarning($"Banner failed to load: {message}")
        });
    }

    public void ShowBanner()
    {
        if (string.IsNullOrEmpty(bannerPlacementId))
            return;

        Advertisement.Banner.Show(bannerPlacementId, new BannerOptions
        {
            showCallback = () => Debug.Log($"Banner shown: {bannerPlacementId}"),
            clickCallback = () => Debug.Log($"Banner clicked: {bannerPlacementId}"),
            hideCallback = () => Debug.Log($"Banner hidden: {bannerPlacementId}")
        });
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
}
