using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using Vuforia;

public class GameManager1 : MonoBehaviour
{

    public AssetLabelReference comicbookName;
    private bool mLoaded = false;
    private DataSet mDataset = null;

void Awake()
    {
        Addressables.DownloadDependenciesAsync(comicbookName);
    }
    void Start()
    {
 
        Addressables.InstantiateAsync(comicbookName);
    }


    void Update()
    {
        if (VuforiaRuntimeUtilities.IsVuforiaEnabled() && !mLoaded)
        {
            string externalPath = Application.persistentDataPath;

            if (mDataset == null)
            {
                // First, create the dataset
                ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                mDataset = tracker.CreateDataSet();
            }

            if (mDataset.Load(externalPath, VuforiaUnity.StorageType.STORAGE_ABSOLUTE))
            {
                mLoaded = true;
            }
            else
            {
                Debug.LogError("Failed to load dataset!");
            }
        }
    }

}
