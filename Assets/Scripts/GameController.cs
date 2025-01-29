using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	[Tooltip("Max bubble the player has before the end of the game.")]
	[SerializeField] private int totalBubbles = 10;

	public int TotalBubbles { get; private set; }
	public bool MouseControl { get => mouseControl; set => mouseControl = value; }
	public bool isPaused { get; private set; }
	public float timeDeltaTime { get; private set; }
	public int Score { get; set; } = 0;

	public int WhaleCryCounter { get; set; } = 4;
	public bool IsDoublePoints { get => isDoublePoints; set => isDoublePoints = value; }
	public bool HasExtraWhale { get => hasExtraWhale; set => hasExtraWhale = value; }
	public float DoublePointsCounter { get => doublePointsCounter; set => doublePointsCounter = value; }
	public float ExtraWhaleCounter { get => extraWhaleCounter; set => extraWhaleCounter = value; }

	private GameObject fadePanel;

	private bool hasExtraWhale = false;
	private bool mouseControl = false;
	private bool isDoublePoints = false;

	private float doublePointsCounter;
	private float doublePointsTimeAmount;

	private GameObject extraWhale;
	private float extraWhaleCounter;
	private float extraWhaleTimeAmount;

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
		TotalBubbles = totalBubbles;
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
				doublePointsCounter = 0;
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

		if (Input.GetButtonDown("Pause"))
		{
			if (!isPaused)
			{
				PauseGame();
			}
			else
			{
				ResumeGame();
			}
		}
	}

	public void resetGame() {
		Score = 0;
		TotalBubbles = totalBubbles;
	}

	public void SubtractBubbles() {
		TotalBubbles--;
		if (TotalBubbles <= 0) {
			LevelManager.instance.LoadLevel("Lose Level");
		}
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
		if (doublePointsCounter > 0)
		{
			doublePointsCounter += time;
		}
		else {
			doublePointsCounter = doublePointsTimeAmount;
		}

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
