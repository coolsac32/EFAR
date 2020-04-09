using UnityEngine;

public class CapturePhotoControllerIOS : MonoBehaviour
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
        //captureAndSave.CaptureAndSaveToAlbum();
    }

    private void SuccessCapturePhoto(string msg)
    {
        //Give Success message
        uiToBeHidden.SetActive(true);
    }

    private void FailCapturePhoto(string msg)
    {
        //Give fail message
        uiToBeHidden.SetActive(true);
    }
}
