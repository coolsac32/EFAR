using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AutofocusTrigger : MonoBehaviour
{
        private bool mAutofocusEnabled = true;
        private bool focusing = false;
        public static bool DoubleTap
    {
        get { return Input.touchSupported && (Input.touches.Length > 0) && (Input.touches[0].tapCount == 2); }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
               if (DoubleTap && !this.focusing)
        {
            this.focusing = true;
            TriggerAutofocusEvent();
        }
    }

        public void TriggerAutofocusEvent()
    {
        StatusMessage.Instance.Display("Manual Focus Triggered", true);
        
        // Trigger an autofocus event
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);

        // Then restore original focus mode
        StartCoroutine(RestoreOriginalFocusMode());
    }

        private IEnumerator RestoreOriginalFocusMode()
    {
        // Wait 1.5 seconds
        yield return new WaitForSeconds(1.5f);

        // Restore original focus mode
        if (mAutofocusEnabled)
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        else
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);

        this.focusing = false;
    }
}
