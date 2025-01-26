using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	[SerializeField] private float maxRateOverTime = 30f;
    [SerializeField] private AudioClip spraySound; // The sound to play when emitting particles

    private ParticleSystem myParticleSystem;
	private ParticleSystem.MainModule main;
	private ParticleSystem.EmissionModule emission; 
    private AudioSource audioSource; // AudioSource to play the sound

    private float inputRightTrigger = 0;


	void Start()
    {
		myParticleSystem = GetComponent<ParticleSystem>();
		main = myParticleSystem.main;
		emission = myParticleSystem.emission;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component on the same GameObject
    }

    void Update()
    {
		if (!GameController.instance.MouseControl ? Input.GetKey(KeyCode.UpArrow) : Input.GetMouseButton(0))
		{
			Debug.Log("key or mouse");
			if (emission.rateOverTime.constant < maxRateOverTime)
			{
				inputRightTrigger += 1f * Time.deltaTime; // Increase value over time
			}

			PlaySprayAudio();
        }
		else {
			inputRightTrigger = Input.GetAxis("Trigger");

			if (inputRightTrigger > 0) {
				PlaySprayAudio();
			}
		}
		if (!GameController.instance.MouseControl ? Input.GetKeyUp(KeyCode.UpArrow) : Input.GetMouseButtonUp(0))
		{
			inputRightTrigger = 0;
		}

		//Debug.Log(inputRightTrigger);
		//main.startLifetime = inputRightTrigger * maxRateOverTime;
		emission.rateOverTime = inputRightTrigger * maxRateOverTime;

	}

	private void PlaySprayAudio() {
		// Play the spray sound if the particles are emitting
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(spraySound); // Play the sound when the emission starts
		}
	}
}
