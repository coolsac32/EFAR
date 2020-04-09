using UnityEngine;
using System.Collections;
using System.IO;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif 

public class CapturePhotoController : MonoBehaviour
{
    public CaptureAndSave captureAndSave;
    public GameObject uiToBeHidden;
    public GameObject uiToBeHidden2;
    public GameObject uiToBeHidden3;
    public GameObject uiToBeShown;

   	void Start()
	{
	
	}
    private void OnEnable()
    {
        CaptureAndSaveEventListener.onSuccess += SuccessCapturePhoto;
        CaptureAndSaveEventListener.onError += FailCapturePhoto;
    }

    private void OnDisable()
    {
        CaptureAndSaveEventListener.onSuccess -= SuccessCapturePhoto;
        CaptureAndSaveEventListener.onError -= FailCapturePhoto;
    }

    public void CapturePhoto()
    {
        uiToBeHidden.SetActive(false);
        uiToBeHidden2.SetActive(false);
        uiToBeHidden3.SetActive(false);
        uiToBeShown.SetActive(true);
        
        captureAndSave.CaptureAndSaveToAlbum();
        //captureAndSave.CaptureAndSaveAtPath(System.IO.Path.Combine(Application.persistentDataPath,"Image.jpg"),ImageType.JPG);

    }


    private void SuccessCapturePhoto(string msg)
    {
#if PLATFORM_ANDROID
        AndroidNativeFunctions.ShowToast("Photo Saved to Gallery");
#endif 
        uiToBeHidden.SetActive(true);
        uiToBeHidden2.SetActive(true);
        uiToBeHidden3.SetActive(true);
        uiToBeShown.SetActive(false);
    }

    private void FailCapturePhoto(string msg)
    {
#if PLATFORM_ANDROID
        AndroidNativeFunctions.ShowToast(msg);
#endif 
        uiToBeHidden.SetActive(true);
        uiToBeHidden2.SetActive(true);
        uiToBeHidden3.SetActive(true);
        uiToBeShown.SetActive(false);
    }
}
