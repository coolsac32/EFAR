using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookScript : MonoBehaviour
{
    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    //Log AR Portal Event
    public void LogAR_PortalEvent()
    {
        FB.LogAppEvent(
            "AR_Portal"
        );
    }

    public void LogAR_World_BigEvent()
    {
        FB.LogAppEvent(
            "AR_World_Big"
        );
    }

    public void LogInfoEvent()
    {
        FB.LogAppEvent(
            "Info"
        );
    }

    //Log AR World Event
    public void LogAR_WorldEvent()
    {
        FB.LogAppEvent(
            "AR_World"
        );
    }

    //Log Digital Comics Event
    public void LogDigital_ComicsEvent()
    {
        FB.LogAppEvent(
            "Digital_Comics"
        );
    }

    public void LogTutorialsEvent()
    {
        FB.LogAppEvent(
            "Tutorials"
        );
    }

    public void LogWebsiteEvent()
    {
        FB.LogAppEvent(
            "Website"
        );
    }

    public void LogExit_ButtonEvent()
    {
        FB.LogAppEvent(
            "Exit_Button"
        );
    }

}
