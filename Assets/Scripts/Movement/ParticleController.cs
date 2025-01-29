using UnityEngine;

public class ParticleController : MonoBehaviour
{
	[Tooltip("Max amount of particles to emit")]
	[SerializeField] private float maxRateOverTime = 30f;
	[Tooltip("The sound to play when emitting particles")]
    [SerializeField] private AudioClip spraySound;

    private ParticleSystem myParticleSystem;
	private ParticleSystem.MainModule main;
	private ParticleSystem.EmissionModule emission; 
    private AudioSource audioSource;

    private float inputRightTrigger = 0;

	void Start()
    {
		myParticleSystem = GetComponent<ParticleSystem>();
		main = myParticleSystem.main;
		emission = myParticleSystem.emission;
        audioSource = GetComponent<AudioSource>();
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

		emission.rateOverTime = inputRightTrigger * maxRateOverTime;
	}

	private void PlaySprayAudio() {
		if (!audioSource.isPlaying && !GameController.instance.isPaused)
		{
			audioSource.PlayOneShot(spraySound); // Play the sound when the emission starts
		}
	}
}
