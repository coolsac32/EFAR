using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;


public class MultiRecoScript : MonoBehaviour, ITrackableEventHandler 
{


    public AudioClip musicFx;
    public Animator[] myAnimator;
    //private TrackableSettings m_TrackableSettings;
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
                animator.speed = 1f;
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

    public void pauseAnim()
    {
        // *** Start of animation code

        foreach (Animator animator in myAnimator)
        {
            animator.speed = 0f;
        }
  
        // *** end of animation code
    }

      
    public void resumeAnim()
    {

        // *** Start of animation code

        foreach (Animator animator in myAnimator)
        {
            animator.speed = 1f;
        }

        // *** end of animation code
    }
}
