using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private GameObject pausePanel;
	[Space]
	[SerializeField] private TextMeshProUGUI ScoreText;

	private Animator animator;

	private void Start()
	{
		
	}

	private void Update()
	{
		ScoreText.text = GameController.instance.Score.ToString();

		if (GameController.instance.isPaused) {
			pausePanel.SetActive(true);
		}
		else {
			pausePanel.SetActive(false);
		}
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
}
