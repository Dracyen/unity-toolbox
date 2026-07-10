using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour
{
	string format;
	
	void Start () {
		format = "yyyy-MM-dd-HH-mm-ss"; 
	}
	
	void Update ()
	{
		//if(Input.GetKeyDown(KeyCode.Space))
		//{
		//	string path = Application.dataPath + "/" + System.DateTime.Now.ToString(format) + ".png";

		//	ScreenCapture.CaptureScreenshot(path);
		//	Debug.Log("Saved Screenshot to " + path);
		//}
	}
}