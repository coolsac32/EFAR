using UnityEngine;
using System.Collections;

public class BrowserOpener : MonoBehaviour {

	public string pageToOpen = "http://www.google.com";

	// check readme file to find out how to change title, colors etc.
	public void OnButtonClicked() {
		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.displayURLAsPageTitle = false;
        options.hidesHistoryButtons = true;
        options.pageTitle = "Eyelnd Browser";
        options.backButtonText = "Exit";
        options.textColor = "#ff8578";

        InAppBrowser.OpenURL(pageToOpen, options);


    }

	public void OnClearCacheClicked()
    {
		InAppBrowser.ClearCache();
	}
}
