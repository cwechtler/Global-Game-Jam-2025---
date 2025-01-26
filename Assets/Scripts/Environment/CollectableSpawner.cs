using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Collectable
{
	public GameObject Prefab;
	[Tooltip("Amount of time between spawns.")]
	public float spawnTime = 10;
	[Tooltip("Minimum allowed distance to any other Collectable.")]
	public float minDistance = 2.0f;
	[Tooltip("Should the Item spawn as soon as the game launches?")]
	public bool spawnOnLaunch;
	[HideInInspector]
	public float collectableCounter; // Individual counter for each instance

	public Collectable(GameObject prefab, float spawnTime, float minDistance, bool spawnOnLaunch)
	{
		Prefab = prefab;
		this.spawnTime = spawnTime;
		this.minDistance = minDistance;
		collectableCounter = 0f; // Initialize the counter here
		this.spawnOnLaunch = spawnOnLaunch;
	}
}

public class CollectableSpawner : MonoBehaviour
{
	[SerializeField] private Collectable[] collectables;
	[Space]
	[Tooltip("Farthest up from the center the prefabs can randomly spawn.")]
	[Range(-3f, 0f)]
	[SerializeField] private float spawnerMaxRange = -3f;
	[Tooltip("Farthest bottom from the center the prefabs can randomly spawn.")]
	[Range(0f, 4f)]
	[SerializeField] private float spawnerMinRange = 4f;

	private Transform lastItem;
	private List<Transform> transforms = new List<Transform>();

	void Start()
	{
		for (int i = 0; i < collectables.Length; i++)
		{

			if (collectables[i].spawnOnLaunch) {
				collectables[i].collectableCounter = collectables[i].spawnTime;
				SpawnCollectable(collectables[i]);
			}
		}
	}

	void Update()
	{
		for (int i = 0; i < collectables.Length; i++) {
			// Directly modify the counter in the collectables array
			collectables[i].collectableCounter += Time.deltaTime;
			SpawnCollectable(collectables[i]);
		}
	}

	private void SpawnCollectable(Collectable collectable) {
		// Remove null or destroyed transforms from the list
		transforms.RemoveAll(t => t == null);

		if (collectable.collectableCounter > collectable.spawnTime)
		{
			Vector3 spawnPosition = GetValidSpawnPosition(collectable);
			if (spawnPosition != Vector3.zero)
			{
				lastItem = Instantiate(collectable.Prefab, spawnPosition, Quaternion.identity).transform;
				lastItem.transform.parent = this.transform;
				transforms.Add(lastItem);
				collectable.collectableCounter = 0f;
			}
			else
			{
				Debug.LogWarning("Could not find a valid spawn position.");
			}
		}
	}

	private Vector3 GetValidSpawnPosition(Collectable collectable)
	{
		// Set the potential position with fixed X and Z and a random Y coordinates.
		Vector3 potentialPosition = new Vector3(11, Random.Range(spawnerMaxRange, spawnerMinRange), 1);

		// Check distance to all objects in the list.
		foreach (Transform objTransform in transforms)
		{
			float distance = Vector3.Distance(potentialPosition, objTransform.position);
			if (distance < collectable.minDistance)
			{
				return Vector3.zero; 
			}
		}

		return potentialPosition;
	}
}
