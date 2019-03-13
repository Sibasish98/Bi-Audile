using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuhandler : MonoBehaviour
{
	float movement = 0f;
	int direction = 1;
	float rateOfAudioMovement = 0f;
	public AudioSource movingsoundsource;
	void Start()
	{
	}
	void Update()
	{
		if (direction == 1) 
		{
			movement +=  Time.deltaTime;
			if (movement >= 2f) 
			{
				direction = 2;
			}
		} 
		else if (direction == 2) 
		{
			movement -=  Time.deltaTime;
			if (movement <= -2f) 
			{
				direction = 1;
			}
		}
		movingsoundsource.panStereo = movement;
	}
}
