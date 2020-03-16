using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using UnityEngine.Networking;
using Vuforia;
using UnityEngine.ResourceManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetDownloader : MonoBehaviour
{

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
}
