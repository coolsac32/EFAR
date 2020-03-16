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

public class CreatedAssetsSimple : MonoBehaviour
{

    private void Start()
    {
        CreateAndWaitUntilCompleted("ARPages");
    }

    private async Task CreateAndWaitUntilCompleted(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var VARIABLE in locations)
            await Addressables.InstantiateAsync(locations).Task;
    }
}
