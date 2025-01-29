using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private GameObject pausePanel;
	[Space]
	[SerializeField] private TextMeshProUGUI ScoreText;
	[SerializeField] private TextMeshProUGUI bubbleCountText;
	[SerializeField] private TextMeshProUGUI doubleScoreText;
	[SerializeField] private TextMeshProUGUI extraWhaleTimerText;
	[SerializeField] private Toggle mouseControl;

	private Animator animator;

	private void Update()
	{
		ScoreText.text = GameController.instance.Score.ToString();
		doubleScoreText.text = GameController.instance.DoublePointsCounter.ToString("0.00");
		extraWhaleTimerText.text = GameController.instance.ExtraWhaleCounter.ToString("0.00");
		bubbleCountText.text = GameController.instance.TotalBubbles.ToString();

		if (GameController.instance.isPaused)
		{
			pausePanel.SetActive(true);
		}
		else
		{
			pausePanel.SetActive(false);
		}

		// For Mouse Controls button on Level 1 for testing only.  Must remain commented out for normal play.
		//GameController.instance.MouseControl = mouseControl.isOn;
	}

	public void MainMenu()
	{
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);
	}

	public void Options()
	{
		animator.SetBool("FadeOut", true);
		LevelManager.instance.LoadLevel(LevelManager.OptionsString, .9f);
	}

	public void QuitGame()
	{
		LevelManager.instance.QuitRequest();
	}


	// to delete after adding win or loose conditions.
	public void LoseGame()
	{
		LevelManager.instance.LoadLevel(LevelManager.LoseLevelString);
	}
	public void WinGame()
	{
		LevelManager.instance.LoadLevel(LevelManager.WinLevelString);
	}
}
