using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughDetector : MonoBehaviour
{
	private bool enteredLeft = false;
	private bool enteredRight = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bubble")){
			// Determine entry side based on bubble's position relative to the ring
			if (other.transform.position.x < transform.position.x){
				enteredLeft = true;
				//Debug.Log("Bubble entered from the left");
			} else{
				enteredRight = true;
				//Debug.Log("Bubble entered from the right");
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Bubble")) {
			// Determine exit side based on bubble's position relative to the ring
			if (other.transform.position.x > transform.position.x) {
				Debug.Log("Bubble exited to the right");
				if (enteredLeft) {
					Score();
				}
			} else {
				Debug.Log("Bubble exited to the left");
				if (enteredRight) {
					Score();
				}
			}

			// Reset flags for the next pass
			enteredLeft = false;
			enteredRight = false;
		}
	}

	private void Score() {
		GameController.instance.Score++;
	}
}


//public class PassThroughDetector : MonoBehaviour
//{
//	private Dictionary<Collider2D, float> entryPoints = new Dictionary<Collider2D, float>();

//	private void OnTriggerEnter2D(Collider2D collision)
//	{
//		if (collision.gameObject.CompareTag("Bubble"))
//		{
//			// Store the object's entry position (e.g., X position for horizontal movement)
//			if (!entryPoints.ContainsKey(collision))
//			{
//				entryPoints[collision] = collision.transform.position.x;  // Change to .y for vertical detection
//			}
//		}
//	}

//	private void OnTriggerExit2D(Collider2D collision)
//	{
//		if (collision.gameObject.CompareTag("Bubble"))
//		{
//			if (entryPoints.TryGetValue(collision, out float entryX))
//			{
//				float exitX = collision.transform.position.x;  // Change to .y for vertical detection

//				// The object fully passes through if it exits on the opposite side
//				if (Mathf.Abs(exitX - entryX) > 0.1f)  // 0.1f prevents false detections due to small position changes
//				{
//					Debug.Log($"{collision.gameObject.name} has fully passed through!");
//					GameController.instance.Score++;
//				}
//				else
//				{
//					Debug.Log($"{collision.gameObject.name} entered and exited from the same side!");
//				}

//				// Remove from dictionary to allow future detections
//				entryPoints.Remove(collision);
//			}
//		}
//	}
//}
