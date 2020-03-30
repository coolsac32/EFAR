using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class SliceLoader : MonoBehaviour
{
    /// Event will be executed once a label finished download. The label value will get passed as an argument
    public static event System.Action<string> OnDownloadComplete;

    [SerializeField] public AssetLabelReference[] listLabels;
    [SerializeField] public AsyncOperationHandle[] listOps;

    float[] downloadProgress;

    private void Awake()
    {
        if (listLabels == null || listLabels.Length < 1)
        {
            Debug.LogError("List labels is empty");
            return;
        }

        downloadProgress = listLabels.Select( x => -10f ).ToArray();
        listOps = new AsyncOperationHandle[ listLabels.Length ];

        Exdee();
    }

    void Exdee()
    {
        foreach( ResourceLocationMap map in Addressables.ResourceLocators )
        {
            Debug.Log( "Map Locator ID : " + map.LocatorId );
            Debug.Log("Keys : " + string.Join(" ", map.Keys));
            //map.Locate("special", typeof(GameObject), out IList<IResourceLocation> locs );
        }

    }

    int GetLabelIndex( string label )
    {
        int label_index = listLabels.Select(x => x.labelString).ToList().IndexOf(label);

        if ( label_index < 0 ) Debug.LogError( $"Label {label} not found" );
    
        return label_index;
    }

    public float DownloadProgress( string label ) => downloadProgress[ GetLabelIndex( label ) ];

    string GetLabel(int index) => listLabels[index].labelString;

    public void Download( string label )
    {
        var index = GetLabelIndex( label );

        if( AsyncOpExists( listOps[ index ] ) )
        {
            Debug.Log("Download is allready in progress");
            return;
        }

        listOps[ index ] = Addressables.DownloadDependenciesAsync( listLabels[ index ] );
        listOps[ index ].Completed += (x) => {
        
            OnDownloadComplete?.Invoke( GetLabel( index ) );

        };

        StartCoroutine( CRTDownload( index ) );
    }

    IEnumerator CRTDownload( int index )
    {
        var op = listOps[ index ];

        downloadProgress[ index ] = 0f;

        while( ! op.IsDone )
        {
            downloadProgress[ index ] = op.PercentComplete;
            yield return new WaitForEndOfFrame();
        }

        downloadProgress[ index ] = 1f;
    }

    bool AsyncOpExists(AsyncOperationHandle op) => !op.Equals(default(AsyncOperationHandle));
}
