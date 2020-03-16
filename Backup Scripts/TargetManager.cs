using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class TargetManager : MonoBehaviour
{
    public string mDatabaseName = "";

    private List<TrackableBehaviour> mAllTargets = new List<TrackableBehaviour>();

    // Start is called before the first frame update
    private void Awake()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
    }


    private void OnVuforiaStarted()
    {
        // Load database
        LoadeDatabase(mDatabaseName);

        // Get trackable targets
        mAllTargets = GetTargest();

        //Setup targets
        SetupTargets(mAllTargets);
    }

    private void LoadeDatabase (string setName)
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<objectTracker>();

        objectTracker.Stop();

        if(DataSet.Exists(setName))
        {
            DataSet dataSet = objectTracker.CreateDataSet();

            dataSet.Load(setName);
            objectTracker.ActivateDataSet(dataSet);
        }

        objectTracker.Start();
    }

    private List<TrackableBehaviour> GetTargest()
    {
        List<TrackableBehaviour> allTrackables = new List<TrackableBehaviour>();
        allTrackables = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();

            // TODO: Change This
        return allTrackables;
    }

    private void SetupTargets(List<TrackableBehaviour> allTargets)
    {
        foreach(TrackableBehaviour target in allTargets)
        {
            // Parent
            target.gameObject.transform.parent = transform;

            // Rename
            target.gameObject.name = target.TrackableName;

            // Add Functionality

            // Done
            Debug.Log(target.TrackableName + " " + "Created");

        }
    }
}
