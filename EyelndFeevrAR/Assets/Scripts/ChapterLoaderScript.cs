using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement;
using Vuforia;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Firebase.Extensions;
using RobinBird.FirebaseTools.Storage.Addressables;

public class ChapterLoaderScript : MonoBehaviour
{

    public string catalogPath;
    public AssetLabelReference comicPrefab;
    public List<IResourceLocation> comics;
   
   void Awake()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Addressables.LoadAssets<IResourceLocation>(comicPrefab.labelString, null);//.Completed += assetReady;

        //Addressables.LoadAssetAsync<GameObject>(comics[]);
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageAssetBundleProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageJsonAssetProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageHashProvider());
    }

    //void assetReady(IAsyncOperation<IList<IResourceLocation>> op)
    // {
    //    comics = new List<IResourceLocation>(op.Result);
    //}
    void Start()
    {
       // initAddressables();
    }

    public void LoadAndActivateDataset(string loadThisDataset)
    {
        TrackerManager trackerManager = (TrackerManager)TrackerManager.Instance;
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        //Stop the tracker.
        objectTracker.Stop();

        //Create a new dataset object.
        DataSet dataset = objectTracker.CreateDataSet();

        //Load and activate the dataset if it exists.
        if (DataSet.Exists(loadThisDataset))
        {
            dataset.Load(loadThisDataset);
            objectTracker.ActivateDataSet(dataset);
        }

        //Start the object tracker.
        objectTracker.Start();
    }

    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
