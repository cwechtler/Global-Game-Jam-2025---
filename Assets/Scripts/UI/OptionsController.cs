using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	//turn tips off
	//Difficulty level
	//Key bindings

	[Space]
	[SerializeField] private Slider masterVolumeSlider;
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private Slider sfxVolumeSlider;
	[Space]
	[SerializeField] private Slider startSliderInSelected;

	private CanvasGroup canvasGroup;
	private Animator canvasGroupAnimator;
	private Animator fadeAnimator;

	void Start ()
	{
		startSliderInSelected.Select();

		//if (GameController.instance.isPaused) {
		//	canvasGroup = canvasGroupPanel.GetComponent<CanvasGroup>();
		//	canvasGroupAnimator = canvasGroupPanel.GetComponent<Animator>();
		//	canvasGroupAnimator.SetBool("FadeIn", true);
		//}
		//else {
		//	fadeAnimator = fadePanel.GetComponent<Animator>();
		//}

		GetSavedVolumeKeys();
	}

	void Update()
	{
		SoundManager.instance.ChangeMasterVolume(masterVolumeSlider.value);
		SoundManager.instance.ChangeMusicVolume(musicVolumeSlider.value);
		SoundManager.instance.ChangeSFXVolume(sfxVolumeSlider.value);
	}

	private void GetSavedVolumeKeys()
	{
		if (PlayerPrefs.HasKey("master_volume")) {
			masterVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		}
		else {
			masterVolumeSlider.value = -20f;
		}

		if (PlayerPrefs.HasKey("music_volume")) {
			musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		}
		else {
			musicVolumeSlider.value = 0f;
		}

		if (PlayerPrefs.HasKey("sfx_volume")) {
			sfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
		}
		else {
			sfxVolumeSlider.value = 0f;
		}
	}

	public void MainMenu() {
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);
	}
	
	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (masterVolumeSlider.value);
		PlayerPrefsManager.SetMusicVolume (musicVolumeSlider.value);
		PlayerPrefsManager.SetSFXVolume(sfxVolumeSlider.value);
		LevelManager.instance.LoadLevel(LevelManager.MainMenuString);

		//if (GameController.instance.isPaused)
		//{
		//	GameCanvasController gameCanvasController = FindObjectOfType<GameCanvasController>();
		//	gameCanvasController.FadeCanvas();
		//	canvasGroupAnimator.SetBool("FadeIn", false);
		//}
		//else {
		//	fadeAnimator.SetBool("FadeOut", true);
		//	LevelManager.instance.LoadLevel(0, .9f);
		//}	
	}
	
	public void SetDefaults(){
		masterVolumeSlider.value = -20f;
		musicVolumeSlider.value = 0f;
		sfxVolumeSlider.value = 0f;
	}
}
