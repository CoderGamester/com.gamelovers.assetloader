using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

// ReSharper disable CheckNamespace

namespace GameLovers.AssetLoader
{
	/// <summary>
	/// Helper class with util improvements for loading methods
	/// </summary>
	public static class AssetLoaderUtils
	{
		/// <summary>
		/// Helper method to interpolate over a list of the given <paramref name="tasks"/>.
		/// Returns the first completed task and moves to the next until all tasks are completed.
		/// </summary>
		/// <remarks>
		/// Based on the implementation of a .Net engineer <seealso cref="https://devblogs.microsoft.com/pfxteam/processing-tasks-as-they-complete/"/>
		/// </remarks>
		public static Task<Task<T>>[] Interleaved<T>(IEnumerable<Task<T>> tasks)
		{
			var inputTasks = tasks.ToList();
			var buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
			var results = new Task<Task<T>>[buckets.Length];
			
			for (int i = 0; i < buckets.Length; i++) 
			{
				buckets[i] = new TaskCompletionSource<Task<T>>();
				results[i] = buckets[i].Task;
			}

			int nextTaskIndex = -1;
			Action<Task<T>> continuation = completed =>
			{
				buckets[Interlocked.Increment(ref nextTaskIndex)].TrySetResult(completed);
			};

			foreach (var inputTask in inputTasks)
			{
				inputTask.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}

			return results;
		}

		/// <summary>
		/// Loads any scene in the given <paramref name="path"/> with the given parameter configuration.
		/// To help the execution of this method is recommended to request the asset path from an <seealso cref="AddressableConfig"/>.
		/// This method can be controlled in an async method and returns the asset loaded
		/// </summary>
		public static async Task<SceneInstance> LoadSceneAsync(string path, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true)
		{
			var operation = Addressables.LoadSceneAsync(path, loadMode, activateOnLoad);

			await operation.Task;

			if (operation.Status != AsyncOperationStatus.Succeeded)
			{
				throw operation.OperationException;
			}
			
			return operation.Result;
		}

		/// <summary>
		/// Unloads the given <paramref name="scene"/> from the game memory.
		/// This method can be controlled in an async method
		/// </summary>
		public static async Task UnloadSceneAsync(SceneInstance scene)
		{
			var operation = Addressables.UnloadSceneAsync(scene);

			await operation.Task;

			if (operation.Status != AsyncOperationStatus.Succeeded)
			{
				throw operation.OperationException;
			}
		}

		/// <inheritdoc cref="IAssetLoader.LoadAssetAsync{T}"/>
		public static async Task<T> LoadAssetAsync<T>(string path)
		{
			var operation = Addressables.LoadAssetAsync<T>(path);

			await operation.Task;

			if (operation.Status != AsyncOperationStatus.Succeeded)
			{
				throw operation.OperationException;
			}
			
			return operation.Result;
		}

		/// <inheritdoc cref="IAssetLoader.InstantiatePrefabAsync"/>
		public static async Task<GameObject> InstantiatePrefabAsync(string path, InstantiationParameters instantiateParameters = new InstantiationParameters())
		{
			var operation = Addressables.InstantiateAsync(path, instantiateParameters);

			await operation.Task;

			if (operation.Status != AsyncOperationStatus.Succeeded)
			{
				throw operation.OperationException;
			}
			
			return operation.Result;
		}
		
		/// <inheritdoc cref="IAssetLoader.UnloadAsset{T}"/>
		public static void UnloadAsset<T>(T asset)
		{
			Addressables.Release(asset);

			var gameObject = asset as GameObject;
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}
}