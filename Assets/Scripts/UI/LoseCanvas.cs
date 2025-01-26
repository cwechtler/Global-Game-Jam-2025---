using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoseCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI total;
	[SerializeField] private GameObject mainMenuButton;

	private Animator animator;
	private bool selected;

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		total.text = GameController.instance.Score.ToString();

	}

	private void Update()
	{
		if (!selected) {
			selected = true;
			EventSystem.current.SetSelectedGameObject(mainMenuButton);
		}
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
