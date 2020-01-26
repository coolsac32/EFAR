using UnityEngine;

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
        AndroidNativeFunctions.ShowToast("Photo Saved to Gallery");
        uiToBeHidden.SetActive(true);
    }

    private void FailCapturePhoto(string msg)
    {
        AndroidNativeFunctions.ShowToast(msg);
        uiToBeHidden.SetActive(true);
    }
}
