using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
  
    public void OpenChannel()
    {
        InAppBrowser.OpenURL("http://www.google.com");
    }
}
