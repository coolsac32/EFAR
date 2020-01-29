using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseManagerScript : MonoBehaviour
{
    public Animator[] myAnimator;
    public Animation[] animationComponents;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponentsInChildren<Animator>();
        animationComponents = GetComponentsInChildren<Animation>();
    }


    public void pauseAnim()
    {
        // *** Start of animation code

        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                animState.speed = 0f;

            }
        }

        // *** end of animation code

        foreach (Animator animator in myAnimator)
        {
            animator.speed = 0f;
        }
    }

    public void resumeAnim()
    {

        // *** Start of animation code

        foreach (Animation animation in animationComponents)
        {
            foreach (AnimationState animState in animation)
            {
                animState.speed = 1f;
                animation.Play();
            }
        }

        foreach (Animator animator in myAnimator)
        {
            animator.speed = 1f;
        }

        // *** end of animation code
    }
}


