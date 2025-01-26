using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundSpawner : MonoBehaviour
{
	[Tooltip("Prefab to spawn.")]
	[SerializeField] private GameObject prefab;
	[Tooltip("The width of the Prefab so the next spawned prefab lines up end to end.")]
	[SerializeField] private float prefabWidth = 10f;
	[Tooltip("How far to the right of the camera the Prefab will spawn.")]
	[SerializeField] private float spawnThreshold = 10f;
	[Space]
	[Tooltip("Position only the inital prefab will spawn in X.")]
	[SerializeField] private float initialXPosition = 0;
	[Tooltip("Position all the prefabs will spawn at in Y.")]
	[SerializeField] private float transformYPosition = 0;

	private Transform lastBackground;

	void Start()
	{
		// Initialize the first background piece
		lastBackground = Instantiate(prefab, new Vector3(initialXPosition, transformYPosition, 1), Quaternion.identity).transform;
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
