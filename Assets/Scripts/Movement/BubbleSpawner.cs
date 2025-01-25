using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
	[SerializeField] private int spawnTime = 10;
	[SerializeField] private int maxTotalBubbles = 5;
	
	private float transformXPosition = 0f;
	private Transform lastBubble;

	private List<Transform> transforms = new List<Transform>();
	private int transformCount = 0;

	private float spawnCounter; // Increase value over time

	void Start()
    {
		transformXPosition = Random.Range(-8f, 8f);
		lastBubble = Instantiate(bubblePrefab, new Vector3(transformXPosition, 6, 1), Quaternion.identity).transform;
		lastBubble.transform.parent = this.transform;
		transforms.Add(lastBubble);
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
			transformXPosition = Random.Range(-8f, 8f);
			lastBubble = Instantiate(bubblePrefab, new Vector3(transformXPosition, 6, 1), Quaternion.identity).transform;
			lastBubble.transform.parent = this.transform;
			transforms.Add(lastBubble);
			transformCount++;
		}

		if (transforms.Count < transformCount){
			transformXPosition = Random.Range(-8f, 8f);
			lastBubble = Instantiate(bubblePrefab, new Vector3(transformXPosition, 6, 1), Quaternion.identity).transform;
			lastBubble.transform.parent = this.transform;
			transforms.Add(lastBubble);			
		}
	}
}
