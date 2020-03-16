using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using UnityEngine.ResourceManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;


public static class CreateAddressablesLoader
{
    public static async Task ByLoadedAddress<T>(IList<IResourceLocation> loadedLocations, List<T> createdObjs)
        where T : Object
    {
        foreach (var location in loadedLocations)
        {
            var obj = await Addressables.InstantiateAsync(location).Task as T;
            createdObjs.Add(obj);
        }
    }

    public static async Task InitAssets<T>(string assetNameOrLabel, List<T> createdAssets)
      where T : Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(assetNameOrLabel).Task;
        foreach (var location in locations)
            createdAssets.Add(await Addressables.InstantiateAsync(location).Task as T);
    }
}