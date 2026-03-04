using UnityEngine;
using UnityEngine.UI;

public class AssetBundleSwap : MonoBehaviour
{
    public AssetBundleLoader loader;

    private bool usingLow = true;

    public void ToggleIcons()
    {
        usingLow = !usingLow;

        if (usingLow)
        {
            loader.bundleName = "storeicons_low";
            loader.assetNames = new string[]
            {
                "rupee_green_low",
                "rupee_yellow_low",
                "wallet_low"
            };
        }
        else
        {
            loader.bundleName = "storeicons_high";
            loader.assetNames = new string[]
            {
                "rupee_green_high",
                "rupee_yellow_high",
                "wallet_high"
            };
        }

        loader.StartCoroutine("LoadBundleFromURL");
    }
}

