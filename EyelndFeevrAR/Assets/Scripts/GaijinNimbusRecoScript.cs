using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GaijinNimbusRecoScript : MonoBehaviour, ITrackableEventHandler
{
    public AudioClip musicFx;

    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            //lancio la musica
            Debug.Log("sound");
            AudioManagerScript.current.PlaySound(musicFx);
        }
        else
        {
            // stoppo la musica
            AudioManagerScript.current.StopSound();
            //  Debug.Log("stoppo");

        }
    }

}
