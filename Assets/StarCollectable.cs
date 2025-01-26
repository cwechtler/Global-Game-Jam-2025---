using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectable : MonoBehaviour
{
	[SerializeField] private int scoreAmount = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bubble"))
		{
			Score();
			Destroy(gameObject);
		}
	}

	private void Score()
	{
		GameController.instance.SetScore(scoreAmount);
		SoundManager.instance.Score();
	}
}
