using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	[SerializeField] private float maxRateOverTime = 30f;

    private ParticleSystem myParticleSystem;
	private ParticleSystem.MainModule main;
	private ParticleSystem.EmissionModule emission;

	private float inputRightTrigger = 0;

	void Start()
    {
		myParticleSystem = GetComponent<ParticleSystem>();
		main = myParticleSystem.main;
		emission = myParticleSystem.emission;
	}

    void Update()
    {
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (emission.rateOverTime.constant < maxRateOverTime)
			{
				inputRightTrigger += 1f * Time.deltaTime; // Increase value over time
			}
		}
		else {
			inputRightTrigger = Input.GetAxis("Trigger");
		}
		if (Input.GetKeyUp(KeyCode.UpArrow)){
			inputRightTrigger = 0;
		}

		//Debug.Log(inputRightTrigger);
		//main.startLifetime = inputRightTrigger * maxRateOverTime;
		emission.rateOverTime = inputRightTrigger * maxRateOverTime;

	}
}
