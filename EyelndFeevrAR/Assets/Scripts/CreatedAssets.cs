using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CreatedAssets : MonoBehaviour
{
    private LoadedAddressableLocations _loadedLocations;

    [field: SerializeField] private List<GameObject> Assets { get; } = new List<GameObject>();
   // [SerializeField] private string _label;
    //[SerializeField] private string _assetName;

   // private List<GameObject> Assets { get; } = new List<GameObject>();

    private void Start()
    {
        CreateAndWaitUntilCompleted();
    }

    private async Task CreateAndWaitUntilCompleted()
    {
        _loadedLocations = GetComponent<LoadedAddressableLocations>();

        await Task.Delay(TimeSpan.FromSeconds(1));

        await CreateAddressablesLoader.ByLoadedAddress(_loadedLocations.AssetLocations, Assets);

       // foreach (var asset in Assets)
       // {
            //ASSET IS CREATED AND IN THE LIST
            //PERFORM ADDITIONAL ACTIONS HERE
       //     Debug.Log(asset.name);
       // }

        //await CreateAddressablesLoader.InitAsset(_label, Assets);
       // await CreateAddressablesLoader.InitAsset(_assetName, Assets);

        foreach (var asset in Assets)
        {
            //OBJS LOADED PERFORM ADDITIONAL ACTIONS
            Debug.Log(asset.name);
        }
    }
}
