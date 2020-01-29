using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MultiRecoScript : MonoBehaviour, ITrackableEventHandler
{
    public AudioClip musicFx;
    public Animator[] myAnimator;

    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
        //Fetch the Animator from your GameObject
        myAnimator = GetComponentsInChildren<Animator>();

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

            foreach (Animator animator in myAnimator)
            {
                animator.gameObject.SetActive(true);
            }
            

        }
        else
        {
            // stoppo la musica
            AudioManagerScript.current.StopSound();
            //  Debug.Log("stoppo");

            foreach (Animator animator in myAnimator)
            {
                animator.gameObject.SetActive(false);
            }
        }
    }
}
