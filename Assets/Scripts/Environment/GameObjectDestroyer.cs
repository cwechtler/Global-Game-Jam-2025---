using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Parallax Layer"))
		{
            Destroy(collision.gameObject);
		}
	}
}
