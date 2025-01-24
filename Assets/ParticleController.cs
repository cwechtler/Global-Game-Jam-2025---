using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	[SerializeField] private float BubbleJetHeight = 7f;
    private ParticleSystem myParticleSystem;
	private ParticleSystem.MainModule main;
	private ParticleSystem.EmissionModule emission;

	private float inputRightTrigger = 0;

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
		//inputRightTrigger = Input.GetAxis("Trigger");
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (emission.rateOverTime.constant < BubbleJetHeight)
			{
				inputRightTrigger += 1f * Time.deltaTime; // Increase value over time
			}
			//inputRightTrigger = 1;
		}
		if (Input.GetKeyUp(KeyCode.UpArrow)){
			inputRightTrigger = 0;
		}
		//Debug.Log(inputRightTrigger);
		//main.startLifetime = inputRightTrigger * BubbleJetHeight;
		emission.rateOverTime = inputRightTrigger * BubbleJetHeight;

	}
}
