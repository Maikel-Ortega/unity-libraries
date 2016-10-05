using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void DownloadCallback   (DownloadedBundle bundleData);
public delegate void OpenCallback       (OpenedBundle bundleData);

public class DownloadedBundle
{
    string id;
    string data1;
    public AssetBundle downloadedBundle;
    public bool ok;
    public DownloadedBundle(bool ok = true)
    {
        this.ok = ok;
    }
    //TODO params
}

public class OpenedBundle
{
    string id;
    string data1;
    public UnityEngine.Object openedBundle;
    //TODO params
}

public interface IAssetBundleLoader  
{
    void DownloadBundle (string path, DownloadCallback callback);
    void OpenBundle     (string bundleID, OpenCallback callback);
    T GetObject<T>(string objectKey) where T:UnityEngine.Object;
    List<UnityEngine.Object> GetAllObjects();
}


