using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDirector : MonoBehaviour
{
	public void CallLoadLevel(string name)
	{
		LevelManager.instance.LoadLevel(name);
	}

	public void StartGame()
	{
		LevelManager.instance.LoadLevel(LevelManager.Level1String, true);
	}

	public void MainMenu()
	{
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);
	}

	public void Quit()
	{
		LevelManager.instance.QuitRequest();
	}

	public void StopAndClearParticles()
	{
		StartCoroutine(StopAndClear());
	}

	private IEnumerator StopAndClear() {
		yield return new WaitForSeconds(.3f);
		ParticleSystem whaleSpout = gameObject.GetComponentInChildren<ParticleSystem>();
		whaleSpout.Stop();
		whaleSpout.Clear();
	}
}
