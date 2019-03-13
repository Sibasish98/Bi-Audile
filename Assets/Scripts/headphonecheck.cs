using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class headphonecheck : MonoBehaviour
{
	AndroidJavaObject acttivityContext;
	AndroidJavaObject head;
	// TMP_Text test;
	// timer = 0f;
	public handler hh;
    // Start is called before the first frame update
    void Start()
	{
		//var plugIN = new AndroidJavaObject ("com.suspiciousrr.headphonetesterr.headphoneee");
		if (!Application.isEditor) 
		{
			if (head == null) 
			{
				using (AndroidJavaClass activityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
					acttivityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				}
			}
			AndroidJavaClass pluginClass = new AndroidJavaClass ("com.suspiciousrr.headphonetesterr.headphoneee");
			head = pluginClass.CallStatic<AndroidJavaObject> ("instance");
			head.Call ("setContext", acttivityContext);
			head.Call ("initialiseAudioo");
			head.Call ("initialiseVibrat");
		}
	}
	public void correctVibrate()
	{
		if (!Application.isEditor) 
		{
			long a = 50;
			head.Call ("vibrateFor", a);
		}
	}
	public void gameoverVibrate()
	{
		if (!Application.isEditor) {
			long a = 500;
			head.Call ("vibrateFor", a);
		}
	}
    // Update is called once per frame
    void LateUpdate()
    {
		if (!Application.isEditor) 
		{
			if (head.Call<bool> ("headsetConnectedd")) 
			{
				//.text = "Headphones connected";
				hh.headPhoneConnected = true;
			} 
			else 
			{
				//test.text = "Please connect headphones";
				hh.headPhoneConnected = false;
			}
		}
    }
}
