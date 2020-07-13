using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

// ReSharper disable CheckNamespace

namespace GameLovers.AssetLoader
{
	/// <summary>
	/// This interface allows to wrap the Addressables Loading scheme into an object reference
	/// </summary>
	public interface IAssetLoader
	{
		/// <summary>
		/// Loads any asset of the given <typeparamref name="T"/> in the given <paramref name="path"/>.
		/// To help the execution of this method is recommended to request the asset path from an <seealso cref="AddressableConfig"/>.
		/// This method can be controlled in an async method and returns the asset loaded
		/// </summary>
		Task<T> LoadAssetAsync<T>(string path);

		/// <summary>
		/// Loads and instantiates the prefab in the given <paramref name="path"/> with the given <paramref name="instantiateParameters"/>.
		/// To help the execution of this method is recommended to request the asset path from an <seealso cref="AddressableConfig"/>.
		/// This method can be controlled in an async method and returns the prefab instantiated
		/// </summary>
		Task<GameObject> InstantiatePrefabAsync(string path, InstantiationParameters instantiateParameters = new InstantiationParameters());

		/// <summary>
		/// Unloads the given <paramref name="asset"/> from the game memory.
		/// If <typeparamref name="T"/> is of <seealso cref="GameObject"/> type, then will also destroy it
		/// </summary>
		void UnloadAsset<T>(T asset);
	}
}