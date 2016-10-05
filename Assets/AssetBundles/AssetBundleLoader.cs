using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AssetBundleLoader :  MonoBehaviour, IAssetBundleLoader
{
    protected Dictionary <string, UnityEngine.Object>   downloadedObjects;
    protected Dictionary <string, AssetBundle>          downloadedBundles;

    /// <summary>
    /// Gets the streaming assets path, because it's different in every device.
    /// </summary>
    /// <returns>The streaming assets path.</returns>
    public string GetStreamingAssetsPath()
    {
        #if UNITY_IPHONE
            return Application.dataPath + "/Raw";
        #elif UNITY_ANDROID
            return jar:file://" + Application.dataPath + "!/assets/";
        #else
            return Application.dataPath + "/StreamingAssets";
        #endif
    }

    #region IAssetBundleLoader implementation

    /// <summary>
    /// Downloads the bundle.
    /// </summary>
    /// <param name="path">Path.</param>
    /// <param name="callback">Callback.</param>
    public void DownloadBundle(string path, DownloadCallback callback)
    {
        StartCoroutine(DownloadAndCache(path,0, callback));
    }

    /// <summary>
    /// Opens the bundle.
    /// </summary>
    /// <param name="bundleID">Bundle I.</param>
    /// <param name="callback">Callback.</param>
    public void OpenBundle(string bundleID, OpenCallback callback)
    {
        if(downloadedBundles.ContainsKey(bundleID))
        {
            AssetBundle ob = downloadedBundles[bundleID];
            AssetBundleRequest request = ob.LoadAllAssetsAsync();
            StartCoroutine(BundleOpeningCoroutine(request, callback));
        }
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns>The object.</returns>
    /// <param name="objectKey">Object key.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T GetObject<T>(string objectKey) where T:UnityEngine.Object
    {
        if(downloadedObjects.ContainsKey(objectKey))
        {
            return (T) downloadedObjects[objectKey];
        }
        else
        {
            throw new Exception( string.Format("Downloaded objects doesn't contain the requested <{0}> item",objectKey));
        }
    }

    /// <summary>
    /// Gets all objects.
    /// </summary>
    /// <returns>The all objects.</returns>
    public List<UnityEngine.Object> GetAllObjects()
    {
        List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
        foreach(string k in downloadedObjects.Keys)
        {
            objects.Add(downloadedObjects[k]);
        }
        return objects;
    }
    #endregion	

    /// <summary>
    /// Downloads the and cache.
    /// </summary>
    /// <returns>The and cache.</returns>
    /// <param name="url">URL.</param>
    /// <param name="version">Version.</param>
    /// <param name="callback">Callback.</param>
    IEnumerator DownloadAndCache (string url, int version, DownloadCallback callback)
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using(WWW www = WWW.LoadFromCacheOrDownload (url,version))
        {
            yield return www;
            if (www.error != null)
            {  
                DownloadedBundle errorBundle = new DownloadedBundle(false);
                Debug.Log("[www] Can't download from url: "+ url);
                callback.Invoke(errorBundle);
            }
            else
            {
                string[] splittedUrl = url.Split(new char[]{'/'},100);
                string bname = splittedUrl[splittedUrl.Length-1];

                Debug.Log("DOWNLOADED BUNDLE: "+bname);

                AssetBundle bundle = www.assetBundle;     
                bundle.name = bname;
                DownloadedBundle newBundle = new DownloadedBundle();
                newBundle.downloadedBundle = bundle;

                //We save our bundle inside a dictionary, using mainasset as bundleid
                if(downloadedBundles == null)
                {
                    downloadedBundles = new Dictionary<string, AssetBundle>();
                }
                Debug.Log("Saving bundle \n -> "+bundle.name);
                downloadedBundles.Add(bundle.name, bundle);

                callback.Invoke(newBundle);
            }
        } 
    }


    /// <summary>
    /// Bundles the opening coroutine.
    /// </summary>
    /// <returns>The opening coroutine.</returns>
    /// <param name="request">Request.</param>
    /// <param name="callback">Callback.</param>
    IEnumerator BundleOpeningCoroutine(AssetBundleRequest request, OpenCallback callback)
    {
        while (!request.isDone)
        {
            Debug.Log("--Opening bundle: "+request.progress+ "%--");
            yield return null;
        }
        
        Debug.Log(">Opening bundle: "+request.progress+ "<");
        
        List<UnityEngine.Object> objects = new List<UnityEngine.Object>(request.allAssets);
        foreach (var item in objects)
        {
            if(downloadedObjects == null)
            {
                downloadedObjects = new Dictionary<string, UnityEngine.Object>();               
            }
            Debug.Log("---- Adding bundle asset \n \\--> "+  item.name);

            downloadedObjects.Add(item.name,item); //TODO: Ojo, se están guardando con el itemName como UnityEngine.Object
        }

        OpenedBundle bundle = new OpenedBundle();
        bundle.openedBundle = request.asset;
        callback.Invoke(bundle);
    }
}
