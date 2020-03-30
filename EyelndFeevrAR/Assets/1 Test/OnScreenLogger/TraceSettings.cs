using UnityEngine;
using TMPro;
using TraceInternal;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Trace
{
    public static bool showDebugLogs = false;

    public static string[] _colors =
    {
        // Color[0] for AppendInt, AppendFloat
        "#ef631c",
        // Color[1] for AppendType, AppendEnum
        "#24d8a5",
    };

    public static bool active => TraceSettings.instance != null;

    public static bool scroll
    {
        get => TraceSettings.instance?.scrollRect.enabled ?? false;
        set { if (active) TraceSettings.instance.scrollRect.enabled = value; }
    }
    public static float height
    {
        get => TraceSettings.instance?.heightScale ?? 0;
        set { if (active) TraceSettings.instance.heightScale = Mathf.Clamp01(value); }
    }

    public static Color backgroundColor
    {
        get => TraceSettings.instance?.background ?? UnityEngine.Color.black;
        set { if (active) TraceSettings.instance.background = value; }
    }

    static Tracer singleton;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void Init() => singleton = new Tracer();

    public class Tracer
    {
        public Tracer() { singleton = this; }

        public Tracer Format( string message, 
            byte useNamedArguments = 0,
            bool newLine = false,
            string color = "", 
            bool bold = false, 
            int fontSize = 0,
            bool italic = false,
            bool underline = false,
            int cspace = 0,
            Color unityColor = default
        )
        {
            string s = message;
            if ( bold ) s = $"<b>{s}</b>";
            if ( italic ) s = $"<i>{s}</i>";
            if ( underline ) s = $"<u>{s}</u>";
            if ( fontSize > 0 ) s = $"<size={fontSize}>{s}</size>";
            if( cspace > 0 ) s = $"<cspace={cspace}>{s}</cspace>";
            if( unityColor != default ) s = $"<color=#{ColorUtility.ToHtmlStringRGB(unityColor)}>{s}</color>";
            else if ( ! string.IsNullOrEmpty(color) ) s = $"<color={(color.IndexOf("#")==0?color:"#"+color)}>{s}</color>";

            return newLine ? Line( s ) : Append( s );
        }

        string feed = "";

        public Tracer Feed( string text = "::clear::" )
        {
            feed += text;
            if( text == "::clear::" ) feed = "";
            if( TraceSettings.instance != null )
                TraceSettings.instance.text = feed;
            return this;
        }

        public Tracer Append( string txt ) => Feed( txt );
        public Tracer Append(params object[] args) => Append(string.Join(" ", args));
        public Tracer NewLine() => Append("\n");
        public Tracer Clear() => Feed();
        public Tracer Line( string txt ) => Feed( txt + "\n" );
        public Tracer Line(params object[] args) => Line( string.Join(" ", args) );
        public Tracer Color(string txt, string color) => Format(txt, color: color);
        public Tracer Color(string txt, Color color) => Format(txt, unityColor : color);
        public Tracer FontSize( string txt, int size ) => Format( txt, fontSize: size );
        public Tracer Bold(string txt) => Format(txt, bold: true);
        public Tracer Italic(string txt) => Format(txt, italic: true);
        public Tracer Underline(string txt) => Format(txt, underline: true);

        public Tracer AppendFloat(float value, int precision = 2)
        => Format(value.ToString("N" + precision), color: _colors[0], underline: true);

        public Tracer AppendInt(int value) => AppendFloat(value, 0);
        public Tracer AppendType(System.Type type) => Format($"[{type.Name }]", color: _colors[1]);

        public Tracer AppendEnum( object enumValue )
        {
            var type = enumValue.GetType();
            if (!type.IsEnum) return Format( "[not_an_enum="+(enumValue.ToString()+"]", color:"red" ));
            var type_name = System.Enum.GetName( type , enumValue );
            return Format($"[{type.Name}:{type_name}:{(int) enumValue }]", color: _colors[1]);   
        }
    }

    public static Tracer Format(
        string txt, byte useNamedArguments = 0, bool newLine = false, 
        string color = "", bool bold = false, int fontSize = 0, bool italic = false, 
        bool underline = false, int cspace = 0, Color unityColor = default ) 
    =>  singleton.Format( txt, 0, newLine, color, bold, fontSize, italic, underline, cspace, unityColor );
    public static Tracer Line(params object[] args) => singleton.Line(args);
    public static Tracer Append(params object[] args) => singleton.Append(args);
    public static Tracer Append(string txt) => singleton.Append(txt);
    public static Tracer NewLine() => Append("\n");
    public static Tracer Clear() => singleton.Clear();
    public static Tracer Color(string txt, string color) => Format(txt, color: color);
    public static Tracer Color(string txt, Color color) => Format(txt, unityColor: color );
    public static Tracer FontSize(string txt, int size) => Format( txt, fontSize: size );
    public static Tracer Bold(string txt) => Format( txt, bold: true );
    public static Tracer Italic(string txt) => Format( txt, italic: true );
    public static Tracer Underline(string txt) => Format( txt, underline: true );
    public static Tracer AppendFloat(float value, int precision = 2) => singleton.AppendFloat(value, precision);
    public static Tracer AppendInt(int value) => singleton.AppendInt(value);
    public static Tracer AppendType(System.Type type) => singleton.AppendType(type);
    public static Tracer AppendEnum(object enumValue) => singleton.AppendEnum( enumValue );
}

#region Extension methods for any unity object

public static class UnityTraceExtension
{
    public static void trace( this Object self, params object[] args)
    {
        bool enabled = Trace.showDebugLogs;
        Trace.showDebugLogs = false;
        Debug.Log(string.Join(" ", args));
        Trace.showDebugLogs = enabled;
        Trace.Line(args);

        //try { throw new System.Exception(); }
        //catch( System.Exception ex )
        //{
        //    Trace.Color( "\t" + string.Join("\n\t", ex.StackTrace.Split('\n')), Color.grey).NewLine();
        //    //foreach (var s in ex.StackTrace.Split('\n')) Trace.Color("\t" + s, Color.grey).NewLine();
        //}
    }

    public static void print(this Object self, params object[] args) => Trace.Line(args);
}

#endregion

namespace TraceInternal
{
    [DisallowMultipleComponent]
    class TraceSettings : MonoBehaviour
    {

        // - - - - - - - - - - - - - - - - - - - - - - - - - 

        #region Inspector Variables

        [SerializeField]
        [Tooltip("Intercept Debug.Log messages and display them")]
        internal bool showNativeDebugLogs = true;

        [Tooltip("How much screen space will the console occupy ? Set value to 1 for a full screen view")]
        [SerializeField, InspectorName("Height"), Range(0.05f, 1)]
        internal float heightScale = 0.20f;

        [Tooltip("Background color includes color opacity (alpha channel)")]
        [SerializeField] internal Color background = Color.black;

        [Tooltip("Use multiple touch points to resize the view")]
        [SerializeField] internal bool multiTouchResize = true;

        [Range(2,4),Tooltip("How much touch points are needed to activate resizing")]
        [SerializeField] internal int touchPointsCount = 2;

        [Tooltip("Do not destory this gameobject on scene reload and keep it alive")]
        [SerializeField] bool doNotDestoy = false;
        
        [Space(10)]
        [Header("Object Reference")]
        [SerializeField] internal TextMeshProUGUI textMeshPro = null;
        [SerializeField] internal ScrollRect scrollRect = null;
        [SerializeField] internal RectTransform viewPort = null;
        [SerializeField] internal Image backgroundImage = null;
        [SerializeField] internal GameObject selfPrefab = null;

        #endregion

        // - - - - - - - - - - - - - - - - - - - - - - - - - 

        #region Variables

        public static TraceSettings instance = null;

        RectTransform backgroundRT;
        RectTransform scrollRT;
        Canvas canvas;
        internal string text = "";
        
        #endregion

        // - - - - - - - - - - - - - - - - - - - - - - - - - 

        #region Mono Behaviour Implementation

        private void OnDestroy() 
        {
            if (instance == this) instance = null; 
        }

        void OnEnable()
        {
            /* 
            -- 
            If doNotDestory is set to one of the object and upon loading a second scene that containes another 
            instance of the same type, delete it and and keep only one tracer alive
            -- 
             */
            //        

            if ( instance != this && ( instance?.gameObject?.scene.buildIndex ?? -2 ) == -1 )
            {
                Debug.LogWarning("Multiple TraceSettings found, destroying current object");

                if ( instance.doNotDestoy && Application.isPlaying )
                {
                    if ( Application.isEditor ) DestroyImmediate( gameObject );
                    else Destroy( gameObject );
                }

                return;
            }

            /*
            --
            Reference self as the static instance, attach to Debugger, and fetch local var references 
            --
             */

            if( Application.isPlaying )
            {
                Application.logMessageReceived += LogMessageReceived;
            }

            canvas = GetComponentInChildren<Canvas>();
            backgroundRT = (RectTransform)backgroundImage.transform;
            scrollRT = (RectTransform)scrollRect.transform;
            textMeshPro = textMeshPro ?? GetComponentInChildren<TextMeshProUGUI>();

            instance = this;
        }

        private void Awake()
        {
            if( Trace.showDebugLogs ) showNativeDebugLogs = Trace.showDebugLogs;
            else if( showNativeDebugLogs ) Trace.showDebugLogs = showNativeDebugLogs;

            if ( doNotDestoy && Application.isPlaying ) 
                DontDestroyOnLoad( gameObject );
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= LogMessageReceived;
        }

        private void LateUpdate()
        {
            if ( ! Application.isPlaying ) return;

            // touch resize
            if (! multiTouchResize || Input.touchCount < touchPointsCount ) return;
            float dt = 0; for( int i = 0; i < touchPointsCount; ++i ) dt += Input.touches[i].deltaPosition.y;
            Trace.height = heightScale - ((dt / touchPointsCount) / Screen.height);
        }

        private void Update()
        {
            // Update view
            textMeshPro.text = text;
            backgroundImage.color = background;
            var viewH = heightScale * Screen.height / canvas.scaleFactor;
            backgroundRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, viewH);
            scrollRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, viewH);
            viewPort.gameObject.SetActive( backgroundRT.sizeDelta.y > 30 );
            if( ! viewPort.gameObject.activeSelf ) return;

            // scroll hack 
            if ((textMeshPro?.renderedHeight ?? 0) <= 0f) return;
            Vector2 size = textMeshPro.rectTransform.sizeDelta;
            size.y = textMeshPro.renderedHeight + (textMeshPro.fontSize + textMeshPro.lineSpacing) * 2f;
            textMeshPro.rectTransform.sizeDelta = size;
        }

#endregion

        void LogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if( ! Trace.showDebugLogs ) return;

            string type_string = $"[Debug.{type.ToString()}]";
            bool trace_stack = false;

            switch (type)
            {
                case LogType.Warning:
                    trace_stack = true;
                    Trace.Color( type_string, "#" + ColorUtility.ToHtmlStringRGB(Color.yellow))
                        .Append(condition)
                        .NewLine();
                    break;
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    trace_stack = true;
                    Trace.Color( type_string, "#" + ColorUtility.ToHtmlStringRGB(Color.red))
                        .Append( condition )
                        .NewLine();
                    break;
                default:
                case LogType.Log:
                    Trace.Line( type_string, condition );
                    break;
            }

            if (trace_stack) Trace.Line("\t", string.Join("\n\t" , stackTrace.Split('\n') ) );
        }
    }
}
