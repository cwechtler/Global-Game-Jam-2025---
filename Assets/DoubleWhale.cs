using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleWhale : MonoBehaviour
{
	[SerializeField] private float extraWhaleTimeAmount;
	[SerializeField] GameObject whalePrefab;

	GameObject player;
	GameObject extraWhale;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bubble"))
		{
			Vector3 spawnOffset = player.transform.right * 3; // Moves 10 units in front
			Vector3 spawnPosition = player.transform.position + spawnOffset;

			extraWhale = Instantiate(whalePrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
			extraWhale.transform.parent = Camera.main.transform;
			GameController.instance.ExtraWhale(extraWhaleTimeAmount, extraWhale);
			SoundManager.instance.ExtraWhale();
			Destroy(gameObject);
		}
	}

}
