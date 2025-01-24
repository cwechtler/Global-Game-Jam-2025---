using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	[SerializeField] private float BubbleJetHeight = 7f;
    private ParticleSystem myParticleSystem;
	private ParticleSystem.MainModule main;
	private ParticleSystem.EmissionModule emission;

	// Start is called before the first frame update
	void Start()
    {
		myParticleSystem = GetComponent<ParticleSystem>();
		main = myParticleSystem.main;
		emission = myParticleSystem.emission;
	}

    // Update is called once per frame
    void Update()
    {
		float inputRightTrigger = Input.GetAxis("Trigger");
		Debug.Log(inputRightTrigger);
		//main.startLifetime = inputRightTrigger * BubbleJetHeight;
		emission.rateOverTime = inputRightTrigger * BubbleJetHeight;

	}
}
