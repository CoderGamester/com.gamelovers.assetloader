using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

// ReSharper disable once CheckNamespace

namespace GameLoversEditor.AssetLoader
{
	/// <summary>
	/// Builds the Addressables before a build
	/// </summary>
	public class AddressablesProcessBuild : IPreprocessBuildWithReport
	{
		public int callbackOrder => 1000;

		public void OnPreprocessBuild(BuildReport report)
		{
			AddressableAssetSettings.BuildPlayerContent();
		}
	}
}