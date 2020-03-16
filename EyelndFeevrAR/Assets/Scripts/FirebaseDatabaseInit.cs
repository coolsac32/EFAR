using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Storage;
using RobinBird.FirebaseTools.Storage.Addressables;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

public class FirebaseDatabaseInit : MonoBehaviour
{
    void Awake()
    {
        Debug.Log($"Current cache: {Caching.defaultCache.path}");

        var cachePaths = new List<string>();
        Caching.GetAllCachePaths(cachePaths);
        foreach (var cachePath in cachePaths)
        {
            Debug.Log($"Cache path: {cachePath}");
        }

        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageAssetBundleProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageJsonAssetProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageHashProvider());

        // Make sure to continue on MAIN THREAD for addressables initialization
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                Debug.Log("FIREBASE INIT FINISHED");
                FirebaseAddressablesManager.IsFirebaseSetupFinished = true;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });


    }
}
