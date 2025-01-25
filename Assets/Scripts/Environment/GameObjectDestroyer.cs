using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.gameObject.name);
		if (!collision.CompareTag("Destroyer"))
		{
			Destroy(collision.gameObject);
		}
	}
}
