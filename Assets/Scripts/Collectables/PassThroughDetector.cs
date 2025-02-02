using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughDetector : MonoBehaviour
{
	[SerializeField] private int scoreAmount = 2;
	[SerializeField] private Color doublePointsColor = new Color(1f, 0f, 0.956f, 1f); // #FF00F4

	private SpriteRenderer[] allChildSpriteRenderers;
	private bool enteredLeft = false;
	private bool enteredRight = false;

	private List<Color> ColorList = new List<Color>(); // Front = #FFF200 : Rear = #FFD700

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
				//Debug.Log("Bubble exited to the right");
				if (enteredLeft) {
					Score();
				}
			} else {
				//Debug.Log("Bubble exited to the left");
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
