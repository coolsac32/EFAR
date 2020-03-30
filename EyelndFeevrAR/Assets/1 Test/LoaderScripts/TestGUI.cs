using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceProviders;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets.Initialization;

[RequireComponent(typeof(SliceLoader))]
class TestGUI : MonoBehaviour
{
    SliceLoader loader;

    void OnEnable() => loader = GetComponent<SliceLoader>();

    private void OnGUI()
    {
        TopRightScope(() => {

            foreach( var lbl in loader.listLabels )
            {
                var label = lbl.labelString;

                var p = loader.DownloadProgress( label );

                if( p < 0 ) // needs download
                {
                    if (BtnClicked("Download " + label))

                        loader.Download( label );
                }
                else if( p >= 1f ) // download complete 
                {
                    BtnClicked("[Downloaded] " + label );
                }
                else if( p >= 0f ) // downloading
                {
                    BtnClicked( ( p * 100 ).ToString("N2") );
                }
            }

            // AddressableLocationLoader
            
        });
    }

    float scale = 1f;
    GUIStyle btnStyle;

    private void Awake()
    {
        scale = Screen.dpi / 96;
    }

    private void TopRightScope( System.Action scope )
    {
        GUILayout.Space(20 * scale);
        using (new GUILayout.HorizontalScope(GUILayout.Width(Screen.width)))
        {
            GUILayout.FlexibleSpace();
            using (new GUILayout.VerticalScope()) scope();
            GUILayout.Space(20 * scale);
        }
    }

    private void InitStyle()
    {
        btnStyle = GUI.skin.textArea;
        btnStyle.fontSize = Mathf.FloorToInt( 14f * scale );
        btnStyle.alignment = TextAnchor.MiddleCenter;
        btnStyle.fixedHeight = 32 * scale;
    }
    bool BtnClicked(string some)
    {
        if( btnStyle == null ) InitStyle();

        GUILayout.Space(10 * scale);
        return GUILayout.Button(some, btnStyle, GUILayout.Width(90f * scale));
    }
}