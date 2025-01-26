using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassBubbleDrop : MonoBehaviour
{
	[SerializeField] private GameObject bubblePrefab;
	[SerializeField] private Color bubbleColor;
	[Tooltip("Time between new bubble spawns.  Value only pertains until the max number of allowable bubbles is hit.")]
	[SerializeField] private int spawnTime = 0;
	[Tooltip("Max # of Bubbles that can be onscreen at the same time.")]
	[SerializeField] private int maxTotalBubbles = 20;
	[Tooltip("Minimum allowed distance to any other bubble.")]
	public float minDistance = 2.0f;
	[Space]
	[Tooltip("Farthest left from the center the bubbles can randomly spawn.")]
	[Range(-8f, 0f)]
	[SerializeField] private float spawnerMinRange = -8f;
	[Tooltip("Farthest right from the center the bubbles can randomly spawn.")]
	[Range(0f, 8f)]
	[SerializeField] private float spawnerMaxRange = 8f;

	private float transformXPosition = 0f;
	private Vector3 spawnPosition;
	private Transform lastBubble;
	private GameObject bubbleParent;

	private List<Transform> transforms = new List<Transform>();

	private void Start()
	{
		bubbleParent = GameObject.FindGameObjectWithTag("Bubble Spawner"); 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bubble"))
		{

			for (int i = 0; i < maxTotalBubbles; i++) {
				SpawnBubble();
			}
			SoundManager.instance.MassBubbles();
			Destroy(gameObject);
		}
	}

	private void SpawnBubble()
	{
		transformXPosition = Random.Range(spawnerMinRange, spawnerMaxRange);
		spawnPosition = GetValidSpawnPosition();
		if (spawnPosition != Vector3.zero)
		{
			lastBubble = Instantiate(bubblePrefab, new Vector3(transformXPosition, 6, 1), Quaternion.identity).transform;
			lastBubble.transform.parent = bubbleParent.transform;
			lastBubble.GetComponent<Bubble>().isValid = false;
			lastBubble.GetComponent <SpriteRenderer>().color = bubbleColor;
			transforms.Add(lastBubble);
		}
		else
		{
			Debug.LogWarning("Could not find a valid spawn position.");
		}
	}
	private Vector3 GetValidSpawnPosition(int maxAttempts = 100)
	{
		// Set the potential position with fixed X and Z and a random Y coordinates.
		Vector3 potentialPosition = new Vector3(Random.Range(spawnerMinRange, spawnerMaxRange), 6, 1);
		int attempts = 0;

		while (attempts < maxAttempts)
		{
			// Set the potential position with fixed X and Z and a random Y coordinates.
			potentialPosition = new Vector3(Random.Range(spawnerMinRange, spawnerMaxRange), 6, 1);
			bool isValid = true;

			// Check distance to all objects in the list.
			foreach (Transform objTransform in transforms)
			{
				float distance = Vector3.Distance(potentialPosition, objTransform.position);
				if (distance < minDistance)
				{
					isValid = false;
					break; // Exit the inner loop if an object is too close
				}
			}

			if (isValid)
			{
				return potentialPosition;
			}

			attempts++;
		}

		Debug.LogWarning("Failed to find a valid spawn position after " + maxAttempts + " attempts.");
		return Vector3.zero; ;
	}
}
