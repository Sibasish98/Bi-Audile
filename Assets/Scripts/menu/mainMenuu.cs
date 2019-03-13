using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenuu : MonoBehaviour
{
	public GameObject title1, title2;
	public GameObject helpPanel;
	bool helpWindowActive = false;
	public GameObject caliborateWindows;
	public GameObject primaryButtons;
	public AudioSource caliborateAudio;
	public AudioSource mainAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (helpWindowActive) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				helpPanel.SetActive (false);
				primaryButtons.SetActive (true);
				helpWindowActive = false;
				title1.SetActive (true);
				title2.SetActive (true);
			}
		}
    }
	public void onPlayButtonClicked()
	{
		mainAudio.Stop ();
		title1.SetActive (false);
		title2.SetActive (false);
		primaryButtons.SetActive (false);
		caliborateWindows.SetActive (true);
	}
	public void onStartGameButtonClicked()
	{
		SceneManager.LoadScene ("gamePlay");
	}
	public void ondirectionCaliborate(string dire)
	{
		
			if (dire == "left") 
			{
				//test on left side
				caliborateAudio.panStereo = -1;
			} 
			else 
			{
				//test on right side
				caliborateAudio.panStereo = 1;
			}
			caliborateAudio.Play ();
	}
	public void onHelpButtonClicked()
	{
		primaryButtons.SetActive (false);
		helpPanel.SetActive (true);
		title1.SetActive (false);
		title2.SetActive (false);
		helpWindowActive = true;
	}
	public void onExitButtonCLicked()
	{
		Application.Quit ();
	}
}
