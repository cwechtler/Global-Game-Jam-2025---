using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughDetector : MonoBehaviour
{
	[SerializeField] private int scoreAmount = 2;
	[SerializeField] private Color doublePointsColor;
	public SpriteRenderer[] allChildSpriteRenderers;
	private bool enteredLeft = false;
	private bool enteredRight = false;

	public List<Color> ColorList = new List<Color>();

	private void Start()
	{
		allChildSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < allChildSpriteRenderers.Length; i++) {
			ColorList.Add(allChildSpriteRenderers[i].color);
		}
	}

	private void Update()
	{
		for (int i = 0; i < allChildSpriteRenderers.Length; i++)
		{
			if (GameController.instance.IsDoublePoints)
			{
				allChildSpriteRenderers[i].color = doublePointsColor; //new Color(1f, 0.478f, 0f, 1f); //#FF7A00
			}
			else
			{
				allChildSpriteRenderers[i].color = ColorList[i]; //new Color(0.922f, 0.765f, 0.612f, 1f);
			}
		}	
	}

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
		GameController.instance.SetScore(scoreAmount);
		SoundManager.instance.Score();
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
