using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundSpawner : MonoBehaviour
{
	[SerializeField] private GameObject prefab; 
	[SerializeField] private float prefabWidth = 10f; 
	[SerializeField] private float spawnThreshold = 10f;
	[SerializeField] private float transformYPosition;

	private Transform lastBackground;

	void Start()
	{
		// Initialize the first background piece
		lastBackground = Instantiate(prefab, new Vector3(0, transformYPosition, 1), Quaternion.identity).transform;
		lastBackground.transform.parent = this.transform;
	}

	void Update()
	{
		// Check if the last background is close to the camera
		if (lastBackground.position.x < Camera.main.transform.position.x + spawnThreshold)
		{
			SpawnNewBackground();
		}
	}

	void SpawnNewBackground()
	{
		// Calculate the new position (right after the last background)
		Vector3 newPosition = new Vector3(lastBackground.position.x + prefabWidth, lastBackground.position.y, lastBackground.position.z);

		// Instantiate the new background
		lastBackground = Instantiate(prefab, newPosition, Quaternion.identity).transform;
		lastBackground.transform.parent = this.transform;

	}
}
