using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public Button showAdButton;
    private string adUnitAffix;

    private void Awake()
    {
#if UNITY_IOS
        adUnitAffix = "_iOS";
#elif UNITY_ANDROID
        adUnitAffix = "_Android";
#elif UNITY_EDITOR
        adUnitAffix = "_Android";
#endif
        showAdButton.interactable = false;
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
        Debug.Log($"{placementId} Completed.");
    }
}
