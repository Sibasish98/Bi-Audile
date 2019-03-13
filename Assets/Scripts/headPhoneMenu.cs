using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class headPhoneMenu : MonoBehaviour
{
	AndroidJavaObject acttivityContext;
	AndroidJavaObject head;
	public GameObject headPhonePannell;
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

	// Update is called once per frame
	void Update()
	{
		if (!Application.isEditor) 
		{
			if (head.Call<bool> ("headsetConnectedd")) 
			{
				headPhonePannell.SetActive (false);
			} 
			else 
			{
				headPhonePannell.SetActive (true);
			}
		}
	}
}
