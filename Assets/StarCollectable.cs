using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectable : MonoBehaviour
{
	[SerializeField] private int scoreAmount = 1;
	[SerializeField] private Color doublePointsColor;

	private SpriteRenderer spriteRenderer;
	private List<Color> ColorList = new List<Color>();

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		ColorList.Add(spriteRenderer.color);
	}

	private void Update()
	{
		if (GameController.instance.IsDoublePoints)
		{
			spriteRenderer.color = doublePointsColor;
		}
		else
		{
			spriteRenderer.color = ColorList[0];
		}
	}

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
