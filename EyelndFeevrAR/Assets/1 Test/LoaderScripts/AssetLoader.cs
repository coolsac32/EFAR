
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

class AssetLoader : MonoBehaviour
{
    public AssetReference reference;

    void Start()
    {
        if( reference != null ) StartMake( reference );
    }

    public void StartMake( AssetReference assetReference, System.Action callback = null )
    {
        StartCoroutine( CoroutineMake( assetReference, callback ) );
    }

    IEnumerator CoroutineMake( AssetReference assetReference, System.Action Callback )
    {
        yield return Make( assetReference, transform );

        Callback?.Invoke();
    }

    public static async Task Make(AssetReference reference, Transform parent = null )
    {
        await reference.InstantiateAsync( parent ).Task;
    }
}
