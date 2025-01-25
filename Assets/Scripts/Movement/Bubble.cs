using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	[Tooltip("# of collisions allowed befor it pops.")]
	[SerializeField] private int collisionsAllowed = 6;
	[Tooltip("Adjust the force applied")]
	[SerializeField] private float forceMultiplier = 0.8f;
	[Tooltip("Max force to apply")]
	[SerializeField] private float maxForce = 1f;
	[Tooltip("Max speed the object can move")]
	[SerializeField] private float maxSpeed = 3f;

	private Rigidbody2D myRigidbody2D;
	private AudioSource audioSource;


	private bool isDead = false;

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!collision.gameObject.CompareTag("Bubble"))
		{
			collisionsAllowed--;
			if (collisionsAllowed == 0)
			{
				myRigidbody2D.velocity = Vector2.zero;

				CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
				circleCollider2D.enabled = false;

				AudioClip clip = SoundManager.instance.BubblePop();
				audioSource.PlayOneShot(clip);

				isDead = true;
				Destroy(gameObject, clip.length);
			}
		}
	}

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	Debug.Log(collision.gameObject.name);
	//	if (collision.gameObject.CompareTag("Collectable"))
	//	{
	//		Debug.Log("Collectable");
	//		GameController.instance.Score++;
	//		Destroy(collision.gameObject);
	//	}
	//}

	private void OnParticleCollision(GameObject other){
		if (myRigidbody2D != null){
			ParticleSystem ps = other.GetComponent<ParticleSystem>();
			if (ps != null){
				List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
				int numCollisionEvents = ps.GetCollisionEvents(gameObject, collisionEvents);

				for (int i = 0; i < numCollisionEvents; i++){
					Vector2 hitVelocity = collisionEvents[i].velocity; // Get particle velocity
					Vector2 forceToApply = Vector2.ClampMagnitude(hitVelocity * forceMultiplier, maxForce);

					myRigidbody2D.AddForce(forceToApply, ForceMode2D.Impulse);
				}

				if (myRigidbody2D.velocity.magnitude > maxSpeed){
					myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * maxSpeed;
				}
			}
		}
	}
}
