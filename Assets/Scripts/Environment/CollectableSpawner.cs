using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
<<<<<<< Updated upstream
	[SerializeField] private GameObject Prefab;
	[Tooltip("Amount of time between spawns")]
	[SerializeField] private int spawnTime = 10;
	[Tooltip("Minimum allowed distance")]
	[SerializeField] private float minDistance = 2.0f;
=======
	[SerializeField] private Collectable[] collectables;
	[Space]
	[Tooltip("Farthest up from the center the prefabs can randomly spawn.")]
	[Range(-3f, 0f)]
	[SerializeField] private float spawnerMaxRange = -3f;
	[Tooltip("Farthest bottom from the center the prefabs can randomly spawn.")]
	[Range(0f, 4f)]
	[SerializeField] private float spawnerMinRange = 4f;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

	private Transform lastItem;
	public List<Transform> transforms = new List<Transform>();
	public float spawnCounter = 0f; //Increase value over time

	void Start()
	{
		lastItem = Instantiate(Prefab, new Vector3(20, Random.Range(-3f, 4f), 1), Quaternion.identity).transform;
		lastItem.transform.parent = this.transform;
		transforms.Add(lastItem);
	}

	void Update()
	{
		// Remove null or destroyed transforms from the list
		transforms.RemoveAll(t => t == null);

		spawnCounter += 1f * Time.deltaTime;
		//Debug.Log(spawnCounter);
		if (spawnCounter > spawnTime){
			SpawnObject();
			spawnCounter = 0f;
		}
	}

	void SpawnObject()
	{
		Vector3 spawnPosition;
		int attempts = 10; // Limit attempts to avoid infinite loops

		do
		{
			spawnPosition = new Vector3(20, Random.Range(-3f, 4f), 1);

			// Check if spawn position is too close to any existing object
			bool tooClose = false;
			foreach (Transform existing in transforms)
			{
				if (Vector3.Distance(spawnPosition, existing.position) < minDistance)
				{
					//print("Too close" + Vector3.Distance(spawnPosition, existing.position));
					tooClose = true;
					break;
				}
			}

			if (!tooClose)
			{
				//print("Spawn");
				lastItem = Instantiate(Prefab, spawnPosition, Quaternion.identity).transform;
				lastItem.transform.parent = this.transform;
				transforms.Add(lastItem);
				return;
			}

			attempts--;
		}
		while (attempts > 0);

<<<<<<< Updated upstream
		Debug.LogWarning("Couldn't find a valid spawn position.");
=======
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
>>>>>>> Stashed changes
	}
}
