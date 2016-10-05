using UnityEditor;

public class AssetBundleLoaderCreator
{
	[MenuItem("Tools/BarbieGenera/Build AssetBundles")]
	static void BuildAllAssetBundles()
	{
		BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.None, BuildTarget.iOS);
	}
}