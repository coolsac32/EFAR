using System.Collections;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement;

using UnityEngine.SceneManagement;

using System;

using System.Collections.Generic;

//using UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle;

//using UnityEngine.AsyncOperation;

using UnityEngine.ResourceManagement.AsyncOperations;

using UnityEngine.AddressableAssets.ResourceLocators;



public class DownloaderServerScript : MonoBehaviour

{

   public GameObject loadingScreen;

    public Slider slider;

    public Text progressText;

     public AssetLabelReference comicbookName;



    public void DownloadfromServer()

    {

        StartCoroutine(LoadAsynchronously());



    }

    // public void UpdatefromServer()
    // {
    //     StartCoroutine(UpdateChecker());
    // }
    
    public void UpdateChecker()
    {
        Addressables.InitializeAsync().Completed += objects =>
        {
            Addressables.CheckForCatalogUpdates().Completed += checkforupdates =>
            {
             if ( checkforupdates.Result.Count > 0)
            
             Addressables.UpdateCatalogs().Completed += updates => DownloadComplete();
             
                else
            
             DownloadComplete();
            };
        };
    }

    private IEnumerator LoadAsynchronously()

    {

        //AsyncOperationHandle asyncOperation = Addressables.DownloadDependenciesAsync(comicbookName);

        var dl = Addressables.DownloadDependenciesAsync(comicbookName);
         dl.Completed += (AsyncOperationHandle) =>
       {
           DownloadComplete();
  
        };

        loadingScreen.SetActive(true);



        //while (!asyncOperation.isDone)
        while (!dl.IsDone)

        {
            Debug.Log("Downloading Asset: " + dl.PercentComplete.ToString());
            float progress = Mathf.Clamp01(dl.PercentComplete / .9f);
            progressText.text = progress * 100f + "%";
            slider.value = progress;
            yield return null;

        }

    }

    private void DownloadComplete()
    {
        Addressables.LoadAssetAsync<GameObject>(comicbookName);
    }

}
