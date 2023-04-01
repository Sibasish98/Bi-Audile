using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
public class handler : MonoBehaviour
{
	//DEFAULT HELP WINDOW
	public GameObject helpPannel;
	public Toggle doNotshowAgain;
	bool helpWindowClosed = false;


	public TMP_Text currentSc, highSc;

	public bool headPhoneConnected = true;
	public GameObject headPhonePannel;


	public GameObject pausePannel;
	public TMP_Text resumeText;
	bool gamePaused = false;
	float resumeTimeout = 4f;
	bool startResumeTimer = false;

	public headphonecheck hc;
	public Slider timmerView;
	float timmer = 0f;
	float currentTimeOut = 4f;
	public TMP_Text scoreTxt;
	public AudioSource beeps;
	public SpriteRenderer[] leftSideSolourholders;
	public SpriteRenderer[] rightSideSolourholders;
	public SpriteRenderer mainColourView;
	bool gameOver = false;
	bool correctHited = false;
	int numberOfColors = 5;
	bool[] leftColourUsed; //R G B
	bool[] rightColourUsed;
	Color[] allColors;
	int currentMainCOlor = -1;
	int score = 0;
    // Start is called before the first frame update

    private void Awake()
    {
		//Permission.RequestUserPermission(Permission.ExternalStorageRead);
    }
    void Start()
    {
		initialiseColourArray ();
		leftColourUsed = new bool[numberOfColors];
		rightColourUsed = new bool[numberOfColors];
		//genertae a random main colour
		scoreTxt.text = "Score "+score.ToString();
		updateMainColor();
		updateColors ();
		beeps.panStereo = -1;
		hc = GetComponent<headphonecheck> ();
		if (PlayerPrefs.GetInt ("helpwindowdisabled") == 0) 
		{
			helpPannel.SetActive (true);
		}
		else 
		{
			helpWindowClosed = true;
			beeps.Play ();
		}
    }
	void initialiseColourArray()
	{
		allColors = new Color[numberOfColors];
		allColors [0] = new Color (0.8f, 0, 0, 1);
		allColors [1] = new Color (0, 0.8f, 0, 1);
		allColors [2] = new Color (0, 0, 0.8f, 1);
		allColors [3] = new Color (0.4f, 0.5f, 0.3f, 1);
		allColors [4] = new Color (0.7f, 0.4f, 0.6f, 1);
	}
	public void okButtonPressedHelp()
	{
		helpPannel.SetActive (false);
		helpWindowClosed = true;
		if (doNotshowAgain.isOn) 
		{
			PlayerPrefs.SetInt ("helpwindowdisabled", 1);
		} 
		else 
		{
			PlayerPrefs.SetInt ("helpwindowdisabled", 0);
		}
		beeps.Play ();
	}
    // Update is called once per frame
    void Update()
    {
		if (headPhoneConnected) {
				if (helpWindowClosed)
				{
			if (!gameOver) {
				if (!gamePaused) {
					handleInput ();
					/*	if (!IsInvoking ("manageAudio")) {
				Invoke ("manageAudio", 3f);
			}*/
					timmer += Time.deltaTime;
					timmerView.value = Mathf.Clamp01 (Mathf.InverseLerp (0f, currentTimeOut, timmer) / .9f);
					//float progress = op.progress / .9f);

					if (timmer >= currentTimeOut) {
						if (correctHited) {
							timmer = 0f;
							correctHited = false;
							if (!beeps.isPlaying) {
								manageAudio ();
								updateColors ();
								updateMainColor ();
							}
						} else {
							hc.gameoverVibrate ();
							gameOver = true;
							handleGameOver ();
						}
					}
				} else {
				
					if (Input.GetMouseButtonDown (0)) {
						if (!startResumeTimer)
							startResumeTimer = true;
					}
					if (startResumeTimer) {
						resumeTimeout -= Time.deltaTime;
						resumeText.text = "           " + ((int)resumeTimeout).ToString ();
						if (resumeTimeout <= 0f) {
							startResumeTimer = false;
							gamePaused = false;
							resumeTimeout = 4f;
							resumeText.text = "TAP TO RESUME";
							pausePannel.SetActive (false);
						}
					}
				}

			} else {
				scoreTxt.text = "Game Over";
			}
		}
		}
		if (!headPhoneConnected) 
		{
			headPhonePannel.SetActive (true);
		}
		else
		{
			headPhonePannel.SetActive (false);
		}
    }
	void manageAudio()
	{
		if (!beeps.isPlaying) 
		{
			changeDirection ();
			beeps.Play ();
		}
	}
	void changeDirection()
	{
		if (Random.Range (1, 3) == 1)
		{
			beeps.panStereo = 1;
		}
		else 
		{
			beeps.panStereo = -1;
		}
	}
	void updateColors()
	{
		int count = 1;
		for (int i = 0; i < leftColourUsed.Length; i++) 
		{
			leftColourUsed [i] = false;
		}
		for (int i = 0; i < rightColourUsed.Length; i++) 
		{
			rightColourUsed [i] = false;
		}
		foreach (SpriteRenderer t in leftSideSolourholders) 
		{
			t.color = assignColor ();
		}
		foreach (SpriteRenderer t in rightSideSolourholders) 
		{
			t.color = assignRightColor ();
		}
		currentTimeOut -= 0.05f;
	}
	Color assignColor()
	{
		int num = 1;//Random.Range (1, 4);
		Color op = Color.black;
		while (true) 
		{
			num = Random.Range (0, numberOfColors);
			if (!leftColourUsed [num])
				break;
		}
		op = allColors [num];
		leftColourUsed [num ] = true;
		return op;
	}
	Color assignRightColor()
	{
		int num = 1;//Random.Range (1, 4);
		Color op = Color.black;
		while (true) 
		{
			num = Random.Range (0, numberOfColors);
			if (!rightColourUsed [num])
				break;
		}
		op = allColors [num];
		rightColourUsed [num] = true;
		return op;
	}
	void handleInput()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit2D hitinfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition),Vector2.zero);
			if (hitinfo) 
			{
				string temp = hitinfo.transform.gameObject.tag;
				if (temp == "left" || temp == "right")
					checkResult (temp,hitinfo.transform.gameObject.GetComponent<SpriteRenderer>().color);
			}
		}
	}
	void checkResult(string tage,Color info)
	{
		if (beeps.panStereo == -1.0f && tage == "left") 
		{
			//clicked on correct left direction and now lets check for colour
			if (mainColourView.color == info) 
			{
				Debug.Log ("icrease score");
				correctHited = true;
				score++;
				hc.correctVibrate ();
				timmer = 0f;
				correctHited = false;
				if (!beeps.isPlaying) 
				{
					manageAudio ();
					updateColors ();
					updateMainColor ();
				}
			}
			else
			{
				Debug.Log ("Worng ");
				gameOver = true;
				handleGameOver ();
				hc.gameoverVibrate ();
			}
		} 
		else if (beeps.panStereo == 1.0f && tage == "right") 
		{
			//clicked on correct right direction and now lets check for colour
			if (mainColourView.color == info) 
			{
				Debug.Log ("icrease score");
				correctHited = true;
				score++;
				hc.correctVibrate ();
				timmer = 0f;
				correctHited = false;
				if (!beeps.isPlaying) 
				{
					manageAudio ();
					updateColors ();
					updateMainColor ();
				}
			}
			else
			{
				Debug.Log ("Worng ");
				gameOver = true;
				handleGameOver ();
				hc.gameoverVibrate ();
			}
		}
		else 
		{
			//clicked on wrong direction
			gameOver = true;
			handleGameOver ();
			hc.gameoverVibrate ();
		}
		scoreTxt.text = "Score "+score.ToString();
	}
	void updateMainColor()
	{
		currentMainCOlor = Random.Range(0,numberOfColors);
		mainColourView.color = allColors [currentMainCOlor];
	}
	public void onpauseButtonClicked()
	{
		gamePaused = true;
		pausePannel.SetActive (true);
	}

	void handleGameOver()
	{
		currentSc.transform.parent.gameObject.SetActive (true);
		if (PlayerPrefs.GetInt ("highscore") < score) 
		{
			PlayerPrefs.SetInt ("highscore", score);
		}
		currentSc.text = "Your Score : " + score.ToString ();
		highSc.text = "High Score : " + PlayerPrefs.GetInt ("highscore");
	}
	public void gameOverButtonPressed(string scName)
	{
		if (scName == "menu")
		SceneManager.LoadScene ("menu");
		else
			SceneManager.LoadScene ("gamePlay");
	}
}