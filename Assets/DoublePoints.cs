using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePoints : MonoBehaviour
{
	public float doublePointsCounter;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		doublePointsCounter += Time.deltaTime;
	}
	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (collision.gameObject.CompareTag("Bubble"))
	//	{
	//		if (collisionsAllowed == 0)
	//		{
	//			myRigidbody2D.velocity = Vector2.zero;

	//			CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
	//			circleCollider2D.enabled = false;

	//			AudioClip clip = SoundManager.instance.BubblePop();
	//			audioSource.PlayOneShot(clip);

	//			Destroy(gameObject, clip.length);
	//		}
	//	}
	//}
}
