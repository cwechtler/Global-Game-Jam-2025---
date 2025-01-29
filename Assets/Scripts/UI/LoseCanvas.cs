using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoseCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI total;

	private Animator animator;

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		total.text = GameController.instance.Score.ToString();
	}


	public void CallLoadLevel(string name)
	{
		LevelManager.instance.LoadLevel(name);
	}

	public void MainMenu()
	{
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);
	}

	public void RestartGame()
	{
		LevelManager.instance.LoadLevel(LevelManager.Level1String, true);
	}

	public void StopAnim() {
		animator.SetBool("Flash", false);
	}
}
