using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
	[SerializeField] private GameObject bubblePrefab;
	[Tooltip("Time between new bubble spawns.  Value only pertains until the max number of allowable bubbles is hit.")]
	[SerializeField] private int spawnTime = 10;
	[Tooltip("Max # of Bubbles that can be onscreen at the same time.")]
	[SerializeField] private int maxTotalBubbles = 1;
	[Space]
	[Tooltip("Farthest left from the center the bubbles can randomly spawn.")]
	[Range(-8f, 0f)]
	[SerializeField] private float spawnerMinRange = -8f;
	[Tooltip("Farthest right from the center the bubbles can randomly spawn.")]
	[Range(0f, 8f)]
	[SerializeField] private float spawnerMaxRange = 8f;

	private float transformXPosition = 0f;
	private Transform lastBubble;

	private List<Transform> transforms = new List<Transform>();
	private int transformCount = 0;

	private float spawnCounter; // Increase value over time

	void Start()
    {
		SpawnBubble();
		transformCount++;
	}

    void Update()
    {
		// Remove null or destroyed transforms from the list
		transforms.RemoveAll(t => t == null);

		if (transformCount > maxTotalBubbles) {
			transformCount = maxTotalBubbles;
		}
		if (transformCount == maxTotalBubbles) {
			spawnCounter = 0f;
		}

		spawnCounter += 1f * Time.deltaTime;

		if (spawnCounter > spawnTime && transforms.Count < maxTotalBubbles) { 
			spawnCounter = 0f;
			SpawnBubble();
			transformCount++;
		}

		if (transforms.Count < transformCount){
			SpawnBubble();
		}
	}

	private void SpawnBubble() {
		transformXPosition = Random.Range(spawnerMinRange, spawnerMaxRange);
		lastBubble = Instantiate(bubblePrefab, new Vector3(transformXPosition, 6, 1), Quaternion.identity).transform;
		lastBubble.transform.parent = this.transform;
		transforms.Add(lastBubble);
	}
}
