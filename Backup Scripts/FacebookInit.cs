using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookInit : MonoBehaviour
{
    public Text FriendsText;

    // Start is called before the first frame update
    void Start()
    {


    }

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

    //Facebook Login
    public void FacebookLogin()
    {

        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    public void FacebookLogout()
    {
        FB.LogOut();
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


    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
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

    //Facebook Share
    public void FacebookShare()
    {
        FB.ShareLink(
    new Uri("https://developers.facebook.com/"), "Join the Adventure!", "Eyelnd Feevr AR App!",
    new System.Uri("https://iltopia.com/wp-content/uploads/2020/01/AR-App-Icon@2x.png"));

    }

    //Facebook Inviting
    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey, check out the new Eyelnd Feevr App!", title: "Eyelnd Feevr AR App");
    }

    public void FacebookInviteAndroid()
    {
        FB.Mobile.AppInvite(new System.Uri("https://play.google.com/store/apps/details?id=com.IltopiaStudios.EyelndFeevr"));
    }

    public void FacebookInviteIOS()
    {
        FB.Mobile.AppInvite(new System.Uri("https://apps.apple.com/us/app/eyelnd-feevr/id1496895598?ls=1"));
    }

    public void GetFriendsPlayingThisGame()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendList = (friendList<object>)dictionary["data"];
            FriendsText.text = string.Empty;
            foreach (var dict in friendList)
                FriendsText.text += ((Dictionary<string, object>)dict)["name"];
        });
    }

    // Update is called once per frame
    void Update()
    {

    }


}
