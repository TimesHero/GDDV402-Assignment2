using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.UnityConsent;

public class AnalyticsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public void GrantConsent()
    {
        EndUserConsent.SetConsentState(new()
        {
            AdsIntent = ConsentStatus.Granted,
            AnalyticsIntent = ConsentStatus.Granted
        });
    }

    public void DenyConsent()
    {
        EndUserConsent.SetConsentState(new()
        {
            AdsIntent = ConsentStatus.Denied,
            AnalyticsIntent = ConsentStatus.Denied
        });
    }

    public void SendCustomEvent()
    {
        
    }
    
    public void SendRupeeClickedEvent(string rupeeColour)
    {
        AnalyticsService.Instance.RecordEvent(new RupeeClickedEvent()
        {
            RupeeColour = rupeeColour
        });
    }


}
