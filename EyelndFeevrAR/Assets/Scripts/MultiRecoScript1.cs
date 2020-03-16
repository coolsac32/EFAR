using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MultiRecoScript1 : MonoBehaviour
{
    public AudioClip musicFx;
    public Animator[] myAnimator;

    //private TrackableBehaviour mTrackableBehaviour;
    private TrackableSettings m_TrackableSettings;
  
    void Start()
    {
        //Fetch the Animator from your GameObject
        myAnimator = GetComponentsInChildren<Animator>();

        m_TrackableSettings = FindObjectOfType<TrackableSettings>();
    }

    void Update()
    {
        if (m_TrackableSettings.IsDeviceTrackingEnabled())
        {

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