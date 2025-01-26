using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePoints : MonoBehaviour
{
	[SerializeField] private float doublePointsTimeAmount;   
	
	private void OnTriggerEnter2D(Collider2D collision)
	{

        if (collision.gameObject.CompareTag("Bubble"))
		{
            GameController.instance.DoublePoints(doublePointsTimeAmount);
			SoundManager.instance.DoubleScore();
			Destroy(gameObject);           
        }
	}
}
