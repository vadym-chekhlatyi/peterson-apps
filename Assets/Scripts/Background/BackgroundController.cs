using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public string bundleUrl = "https://localhost/streamingassets/testbundle";
    //public string remoteAssetName = "BundledSpriteObject";

    public string assetName = "Background"; 
    public string bundleName = "testbundle";

    private void Start()
    {
        StartCoroutine(LoadAsset());
    }

    public void ChangeBackground(int level)
    {
        //assetName = "background" + level;

        LoadAssetFromFile();
        //StartCoroutine(LoadAsset());
    }

    private IEnumerator LoadAsset()
    {
        using (WWW web = new WWW(bundleUrl))
        {
            yield return web;
            Debug.Log(web.text);
            AssetBundle remoteAssetBundle = web.assetBundle;
            if (remoteAssetBundle == null)
            {
                Debug.Log("Failed to download AssetBundle");
                yield break;
            }
            Instantiate(remoteAssetBundle.LoadAsset(assetName), transform);
            remoteAssetBundle.Unload(false);
        }
    }

    private void LoadAssetFromFile()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName)); 
        
        if (localAssetBundle == null) 
        { 
            Debug.LogError("Failed to load AssetBundle!"); 
            return; 
        }

        GameObject asset = localAssetBundle.LoadAsset<GameObject>(assetName); 
        GameObject newBackground = Instantiate(asset, transform);
        newBackground.transform.SetSiblingIndex(0);
        localAssetBundle.Unload(false);
    }
}
