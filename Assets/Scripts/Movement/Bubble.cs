using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	[SerializeField] private float minBoostInterval = 2f;
	[SerializeField] private float maxBoostInterval = 4f;
	[SerializeField] private float minBoostSpeed = 5f;
	[SerializeField] private float maxBoostSpeed = 10f;
	[SerializeField] private float moveDirectionDeviation = 0.35f;
	[SerializeField] private int collisionsAllowed = 6;
	[SerializeField] private float forceMultiplier = 10f; // Adjust the force applied
	[SerializeField] private float maxForce = 2f; // Max force to apply
	[SerializeField] private float maxSpeed = 3f; // Max speed the object can move

	//private BubbleFactory factoryParent;
	private GameObject bubbleAimDirection;	
	private Rigidbody2D myRigidbody2D;

	private AudioSource audioSource;
	private Animator animator;
	private RuntimeAnimatorController animController;

	private bool isDead = false;

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		animator = GetComponentInChildren<Animator>();
		animController = animator.runtimeAnimatorController;

		// if new instance, set velocity of 1 toward aim direction y-axis
		if (myRigidbody2D.velocity.magnitude == 0) {
			//myRigidbody2D.velocity = bubbleAimDirection.transform.up;
		}

		//StartCoroutine(RotateBubble(animController.animationClips.First(a => a.name == "Bubble Spawn").length));
		StartCoroutine(MoveBubble());
	}

	//public void InitializeFromFactory(BubbleFactory _factoryParent, GameObject _bubbleAimDirection)
	//{
	//	this.factoryParent = _factoryParent;
	//	this.bubbleAimDirection = _bubbleAimDirection;
	//}

	public IEnumerator RotateBubble(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		this.transform.rotation = Quaternion.identity;
	}

	private IEnumerator MoveBubble() {
		for (;;) {
			if (!isDead) {
				// boost only if new speed is faster than current speed
				float newSpeed = (Random.value * (maxBoostSpeed - minBoostSpeed)) + minBoostSpeed;
				if (newSpeed > myRigidbody2D.velocity.magnitude) {
					float directionDeviation = (Random.value * moveDirectionDeviation * 2) - moveDirectionDeviation;
					float newDirection = Mathf.Atan2(myRigidbody2D.velocity.normalized.y, myRigidbody2D.velocity.normalized.x) + directionDeviation;
					myRigidbody2D.velocity = new Vector2(Mathf.Cos(newDirection) * newSpeed, Mathf.Sin(newDirection) * newSpeed);
				}
			}
			float nextBoostTime = (Random.value * (maxBoostInterval - minBoostInterval)) + minBoostInterval;
			yield return new WaitForSeconds(nextBoostTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collisionsAllowed --;
		if (collisionsAllowed == 0) {
			//animator.SetTrigger("Bubble Pop");
			myRigidbody2D.velocity = Vector2.zero;

			CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
			circleCollider2D.enabled = false;
			
			//audioSource.PlayOneShot(SoundManager.instance.BubblePop());

			//if (!isDead && factoryParent != null) {
			//	factoryParent.spawnedBubbles--;	
			//}

			isDead = true;
			Destroy(gameObject, animController.animationClips.First(a => a.name == "Bubble Pop").length);
		}
	}
	private void OnParticleCollision(GameObject other)
	{
		//Debug.Log("Particle hit: " + other.name);

		if (myRigidbody2D != null)
		{
			//Debug.Log("Has Rigidbody2D");

			ParticleSystem ps = other.GetComponent<ParticleSystem>();
			if (ps != null)
			{
				//Debug.Log("Has Particle System");

				List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
				int numCollisionEvents = ps.GetCollisionEvents(gameObject, collisionEvents);

				for (int i = 0; i < numCollisionEvents; i++)
				{
					Vector2 hitVelocity = collisionEvents[i].velocity; // Get particle velocity
					Vector2 forceToApply = Vector2.ClampMagnitude(hitVelocity * forceMultiplier, maxForce);

					myRigidbody2D.AddForce(forceToApply, ForceMode2D.Impulse);
					//Debug.Log("Applied force: " + forceToApply);

					//Vector2 hitVelocity = collisionEvents[i].velocity; // Particle velocity
					//myRigidbody2D.AddForce(Vector2.ClampMagnitude(hitVelocity * forceMultiplier, 5f), ForceMode2D.Impulse);
					//Debug.Log("Applied force: " + hitVelocity);

					// Limit max speed
				}

				if (myRigidbody2D.velocity.magnitude > maxSpeed)
				{
					myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * maxSpeed;
				}
			}
		}
	}
}
