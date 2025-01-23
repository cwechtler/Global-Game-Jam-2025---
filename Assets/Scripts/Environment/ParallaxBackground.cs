using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
	[System.Serializable]
	public class ParallaxLayer
	{
		public Transform layer;  // The background layer
		public float speedMultiplier;  // Speed of this layer
	}

	[SerializeField] private ParallaxLayer[] layers;
	[SerializeField] private float speed = 2f; // Simulated movement speed

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
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
