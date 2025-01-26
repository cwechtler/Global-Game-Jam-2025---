//using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	public GameObject playerGO { get; private set; }
	public bool isPaused { get; private set; }
	public float timeDeltaTime { get; private set; }
	public int Score { get; set; } = 0;
	public int whaleCryCounter { get; set; } = 4;

	public bool MouseControl { get => mouseControl; set => mouseControl = value; }
	public bool IsDoublePoints { get => isDoublePoints; set => isDoublePoints = value; }
	public bool HasExtraWhale { get => hasExtraWhale; set => hasExtraWhale = value; }

	private bool hasExtraWhale = false;
	private bool mouseControl = false;
	private bool isDoublePoints = false; 

	private GameObject fadePanel;

	public float doublePointsCounter;
	public float doublePointsTimeAmount;

	public float extraWhaleCounter;
	public float extraWhaleTimeAmount;
	
	private GameObject extraWhale;



	private void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		if (PlayerPrefs.HasKey("mouse_controls")) {
			mouseControl = PlayerPrefsManager.GetMouseControls();
		}
		//extraWhale = GameObject.FindGameObjectWithTag("Extra Whale");
		//extraWhale.SetActive(false);
		//PlayerPrefsManager.DeleteAllPlayerPrefs();
	}

	private void Update()
	{
		if (IsDoublePoints) {
			doublePointsCounter -= Time.deltaTime;

			if (doublePointsCounter <= 0)
			{
				IsDoublePoints = false;
				doublePointsCounter = doublePointsTimeAmount;
			}
		}
		if (hasExtraWhale)
		{
			extraWhaleCounter -= Time.deltaTime;

			if (extraWhaleCounter <= 0)
			{
				hasExtraWhale = false;
				extraWhale.SetActive(false);
				extraWhaleCounter = extraWhaleTimeAmount;
			}
		}
		//if (Input.GetButtonDown("Pause")) {
		//	if (!isPaused) {
		//		PauseGame();
		//	}
		//	else {
		//		ResumeGame();
		//	}
		//}
	}

	public void resetGame() {
		Score = 0;
	}

	public void ExtraWhale(float time, GameObject extraWhale) {
		this.extraWhale = extraWhale;
		extraWhaleTimeAmount = time;
		extraWhaleCounter += time;
		HasExtraWhale = true;
	}

	public void DoublePoints(float time)
	{
		doublePointsTimeAmount = time;
		doublePointsCounter += time;
		IsDoublePoints = true;
	}

	public void SetScore(int amount) {
		if (IsDoublePoints == true)
		{
			Score += (amount * 2);
		}
		else {
			Score += amount;
		}

	}

	public void FadePanel()
	{	
		fadePanel = GameObject.FindGameObjectWithTag("Fade Panel");
		fadePanel.GetComponent<Animator>().SetBool("FadeIn", true);
	}

	private void PauseGame()
	{
		timeDeltaTime = Time.deltaTime;
		isPaused = true;
		Time.timeScale = 0;
	}

	private void ResumeGame()
	{
		Time.timeScale = 1;
		isPaused = false;
	}

	private IEnumerator RespawnPlayer(int waitToSpawn)
	{
		yield return new WaitForSeconds(waitToSpawn);
		//playerGO.transform.position = spawnPoint.transform.position;
		playerGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		playerGO.gameObject.SetActive(true);
		playerGO.GetComponent<Rigidbody2D>().isKinematic = false;
		yield return new WaitForSeconds(1);
	}

	public IEnumerator FadeCanvasGroup_TimeScale_0(CanvasGroup canvasGroup, bool isPanelOpen, float fadeTime)
	{
		float counter = 0f;

		if (isPanelOpen) {
			while (counter < fadeTime) {
				counter += timeDeltaTime;
				canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTime / timeDeltaTime);
			}
		}
		else {
			while (counter < fadeTime) {
				counter += timeDeltaTime;
				canvasGroup.alpha = Mathf.Lerp(0, 1, fadeTime / timeDeltaTime);
			}
		}
		yield return null;
	}
}
