using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Vuforia;

public class LoadAssetsFromRemote : MonoBehaviour
{
    
    [SerializeField] private AssetLabelReference _label;
    
    // Start is called before the first frame update
   private void Start()
    {
        Get(_label);
    }

private async Task Get(AssetLabelReference label)
{
 var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
 foreach (var location in locations)
 {
await Addressables.InstantiateAsync(location).Task;
 }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
