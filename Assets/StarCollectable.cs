using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectable : MonoBehaviour
{
	[SerializeField] private int scoreAmount = 1;
	[SerializeField] private Color doublePointsColor;
	public SpriteRenderer spriteRenderer;
	public List<Color> ColorList = new List<Color>();

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		ColorList.Add(spriteRenderer.color);
	}

	private void Update()
	{
		//for (int i = 0; i < spriteRenderer.Length; i++)
		//{
			if (GameController.instance.IsDoublePoints)
			{
				spriteRenderer.color = doublePointsColor; //new Color(1f, 0.478f, 0f, 1f); //#FF7A00
			}
			else
			{
				spriteRenderer.color = ColorList[0]; //new Color(0.922f, 0.765f, 0.612f, 1f);
			}
		//}
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
