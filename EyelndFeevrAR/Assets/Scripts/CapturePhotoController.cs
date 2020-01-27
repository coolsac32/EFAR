using UnityEngine;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif 

public class CapturePhotoController : MonoBehaviour
{
    public CaptureAndSave captureAndSave;
    public GameObject uiToBeHidden;

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
        captureAndSave.CaptureAndSaveToAlbum();
    }

    private void SuccessCapturePhoto(string msg)
    {
#if PLATFORM_ANDROID
        AndroidNativeFunctions.ShowToast("Photo Saved to Gallery");
#endif 
        uiToBeHidden.SetActive(true);
    }

    private void FailCapturePhoto(string msg)
    {
#if PLATFORM_ANDROID
        AndroidNativeFunctions.ShowToast(msg);
#endif 
        uiToBeHidden.SetActive(true);
    }
}
