using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestAssetBundles : MonoBehaviour {

    public string url = "https://dl.dropboxusercontent.com/u/6538354/AssetBundlesTest/Barbie/test/bundlea";
    public bool tryStreamingAssets = true;
	
    AssetBundleLoader loader;

    void Start () 
    {
        loader = this.GetComponent<AssetBundleLoader>();	
	}
	
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            DownloadAssetBundle();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            ListAllDownloadedItems();
        }
	}

    void ListAllDownloadedItems()
    {
        Debug.Log("<color=red>Command<L>: List All Downloaded Objects</color>");
        List<UnityEngine.Object> ao =  loader.GetAllObjects();
        Debug.Log("Number of downloaded objects: "+ ao.Count);
        Debug.Log("--List-- ");
        foreach (var item in ao)
        {
            Debug.Log(item.name);
        }
    }

    void DownloadAssetBundle()
    {        
        Debug.Log("<color=red>Command<D>: Download test bundle</color>");
        Debug.Log("URL: "+url);
        this.loader.DownloadBundle(url, OnBundleDownloaded);
    }

    void OnBundleDownloaded(DownloadedBundle bundledata)
    {
        if(bundledata.ok)
        {
            Debug.Log("OnBundleDownloaded: "+bundledata.downloadedBundle.name); 
            string id = bundledata.downloadedBundle.name;
            loader.OpenBundle(id,OnBundleOpened);
        }
        else
        {
            Debug.Log("ERROR: Could not download bundle");        
            DownloadFromStreamingAssets();
        }
    }

    void DownloadFromStreamingAssets()
    {
        Debug.Log("Trying streaming assets");
        string path = loader.GetStreamingAssetsPath();
        path += GetLastPartOfUrl(url);  //To obtain the actual name of the asset bundle
        loader.DownloadBundle(path,OnBundleDownloadedFromStreamingAssets);
    }

    void OnBundleDownloadedFromStreamingAssets(DownloadedBundle bundledata)
    {
        if(bundledata.ok)
        {
            Debug.Log("OnBundleDownloadedFromStreamingAssets: "+bundledata.downloadedBundle.name); 
            string id = bundledata.downloadedBundle.name;
            loader.OpenBundle(id,OnBundleOpened);
        }
        else
        {
            Debug.Log("ERROR: Bundle not located on Streaming Assets folder");        
        }
    }
    
    void OnBundleOpened(OpenedBundle bundledata)
    {
        Debug.Log("OnBundleOpened: "+bundledata);
        MockScriptable ms = loader.GetObject<MockScriptable>("BundleB2");
        TestMockScriptable(ms);
    }

    void TestMockScriptable(MockScriptable ms)
    {
        Debug.Log("<< MockScriptable count: "+ms.data.Count);
        string r = "El Quijote: ";
        foreach (string item in ms.data)
        {
            r = r+item;
        }
        Debug.Log(r);        
    }

    string GetLastPartOfUrl(string s)
    {
        string[] splittedUrl = s.Split(new char[]{'/'},100);       
        return  splittedUrl[splittedUrl.Length-1];
    }
}
