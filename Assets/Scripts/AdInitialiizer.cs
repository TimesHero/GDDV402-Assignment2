using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdInitialiizer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameID;
    [SerializeField] private string iOSGameID;
    [SerializeField] private bool testMode = true;

    private string gameID;

    private void InitializeAds()
    {
#if UNITY_IOS
        gameID = iOSGameID;
#elif UNITY_ANDROID
        gameID = androidGameID;
#elif UNITY_EDITOR
        gameID = androidGameID;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, testMode, this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitializeAds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInitializationComplete()
    {
        Debug.Log($"Unity Ads successfully initialized.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogWarning($"Unity Ads Initialization Failed: {error} - {message}");
    }
}
