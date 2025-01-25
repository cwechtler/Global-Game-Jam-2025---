using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughDetector : MonoBehaviour
{
	private Dictionary<Collider2D, float> entryPoints = new Dictionary<Collider2D, float>();

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bubble"))
		{
			// Store the object's entry position (e.g., X position for horizontal movement)
			if (!entryPoints.ContainsKey(collision))
			{
				entryPoints[collision] = collision.transform.position.x;  // Change to .y for vertical detection
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bubble"))
		{
			if (entryPoints.TryGetValue(collision, out float entryX))
			{
				float exitX = collision.transform.position.x;  // Change to .y for vertical detection

				// Check if the object truly passed through (exitX is on the opposite side of entryX)
				if (exitX > entryX) // Adjust condition for your game (e.g., left-to-right or top-to-bottom)
				{
					Debug.Log($"{collision.gameObject.name} has fully passed through!");
					GameController.instance.Score++;
				}
				else
				{
					Debug.Log($"{collision.gameObject.name} entered and exited from the same side!");
				}

				// Remove from dictionary to allow future detections
				entryPoints.Remove(collision);
			}
		}
	}
}
