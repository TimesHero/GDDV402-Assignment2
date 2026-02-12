using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{

    public string bundleName;
    public string variantName;

    public string[] assetNames;
    public Image[] icons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadBundleFromURL());
    }

    private IEnumerator LoadBundleFromURL()
    {
        string extension = string.IsNullOrEmpty(variantName) ? string.Empty : '.' + variantName;
        string url = Path.Combine(Application.streamingAssetsPath, bundleName, extension);

        using UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to download AssetBundle: {webRequest.error}");
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);

        for (int i = 0; i < assetNames.Length; i++)
        {
            yield return StartCoroutine(LoadSpriteFromBundle(bundle, assetNames[i], icons[i]));
        }
        bundle.Unload(false);

    }

    private IEnumerator LoadSpriteFromBundle(AssetBundle bundle, string assetName, Image icon)
    {
        AssetBundleRequest bundleRequest = bundle.LoadAssetAsync<Sprite>(assetName);
        yield return bundleRequest;

        if (bundleRequest.asset != null)
        {
            icon.sprite = (Sprite)bundleRequest.asset;
            Debug.Log($"Loaded {assetName} from {bundleName}");
        }
        else Debug.LogError($"Failed to load {assetName} from {bundleName}");
    }

}
