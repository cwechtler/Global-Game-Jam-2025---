using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
	[System.Serializable]
	public class ParallaxLayer
	{
		[Tooltip("The background layer")]
		public Transform layer;  // The background layer
		[Tooltip("Speed of this layer")]
		public float speedMultiplier;  // Speed of this layer
	}

	[SerializeField] private ParallaxLayer[] layers;
	[Tooltip("Simulated movement speed")]
	[SerializeField] private float speed = 2f;

    void Update()
    {
		foreach (var layer in layers)
		{
			if (layer.layer != null)
			{
				Vector3 newPos = layer.layer.position;
				newPos.x -= speed * layer.speedMultiplier * Time.deltaTime;
				layer.layer.position = newPos;
			}
		}
	}
}
