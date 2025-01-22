//using Pathfinding;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private int caughtTimer = 10;
	[SerializeField] private Vector2 speed = new Vector2(10, 10);
	[Space]
	[SerializeField] private Transform chicken;
	[SerializeField] private GameObject rigFront, rigBack;
	[SerializeField] private AnimationClip eatWormAnimClip;
	[Space]
	[SerializeField] private CanvasController canvasController;
	[Space]
	[SerializeField] private AudioClip playerCaughtClip;
	[SerializeField] private AudioClip eatWormClip;
	[SerializeField] private AudioClip deathClip;

	public float FireX { get => fireX; }
	public float FireY { get => fireY; }

	private Rigidbody2D myRigidbody2D;
	private Animator[] animators;
	private AudioSource audioSource;
	private GameObject farmer;

	private bool moveHorizontaly, moveVertically;
	private bool isDead = false;
	private bool isCaught = false;
	private float timer;
	private bool hasChance = false;
	private bool loadNextLevel = true;
	private Coroutine eat;
	private Animator[] enemyAnimators;
	private Vector3 myPosition;


	private float fireX, fireY;

	void Start()
	{
		farmer = GameObject.FindGameObjectWithTag("Enemy");
		myRigidbody2D = GetComponent<Rigidbody2D>();
		animators = GetComponentsInChildren<Animator>(true);
		audioSource = GetComponent<AudioSource>();
		timer = caughtTimer;
	}

	void Update()
	{
		if (!isDead && !isCaught)
		{
			Move();
			// fire ability here
		}

		if(isDead) { 
			transform.position = myPosition;
		}

		//if (isCaught) {
		//	CountDown();
		//	if ((Input.GetButtonDown("Fire1") || Input.GetMouseButton(1)) && hasChance == false)
		//	{
		//		StartCoroutine(GetAway());
		//	}
		//}
	}

	public void CountDown()
	{
		if (!isDead)
		{
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				StartCoroutine(PlayerDeath());
			}
		}
	}

	private void Move()
	{
		float inputY = Input.GetAxis("Vertical");
		float inputX = Input.GetAxis("Horizontal");

		if (Input.GetMouseButton(0)) {
			Vector3 direction = MousePointerDirection();

			inputX = Mathf.Clamp(direction.x, -1, 1);
			inputY = Mathf.Clamp(direction.y, -1, 1);
		}

		myRigidbody2D.velocity = new Vector2(speed.x * inputX, speed.y * inputY);
		moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
		moveVertically = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

		SetAnimations();
		FlipDirection();
	}

	//private IEnumerator GetAway()
	//{
	//	hasChance = true;
	//	int chance = Random.Range(0, 10);

	//	AIDestinationSetter destinationSetter = farmer.GetComponent<AIDestinationSetter>();
	//	if (chance == 5)
	//	{
	//		audioSource.Stop();
	//		isCaught = false;
	//		myRigidbody2D.isKinematic = false;
	//		myRigidbody2D.velocity = new Vector3(0, 0, 0);
	//		timer = caughtTimer;
	//		canvasController.ReduceCaughthBar(caughtTimer);
	//		canvasController.ActivateCaughtHud(false);
	//		destinationSetter.target = farmer.transform;
	//		foreach (var animator in animators)
	//		{
	//			animator.SetBool("Caught", false);
	//		}
	//		if (enemyAnimators != null)
	//		{
	//			foreach (var enemyAnimator in enemyAnimators)
	//			{
	//				if (enemyAnimator.isActiveAndEnabled)
	//				{
	//					enemyAnimator.SetBool("Catch", false);
	//				}
	//			}
	//		}

	//		yield return new WaitForSeconds(2f);
	//		destinationSetter.target = this.transform;
	//	}

	//	yield return new WaitForSeconds(.2f);
	//	hasChance = false;
	//}

	private IEnumerator PlayerDeath()
	{
		myPosition = transform.position;
		isDead = true;
		myRigidbody2D.isKinematic = true;
		myRigidbody2D.velocity = new Vector3(0, 0, 0);
		audioSource.Stop();
		audioSource.clip = deathClip;
		audioSource.loop = false;
		audioSource.Play();
		
		yield return new WaitForSeconds(deathClip.length - .7f);

		foreach (var animator in animators) {
			animator.SetBool("IsDead", true);
		}
		yield return new WaitForSeconds(2f);
		LevelManager.instance.LoadLevel(LevelManager.LoseLevelString);
	}

	private IEnumerator PlayerCaught()
	{
		isCaught = true;
		myRigidbody2D.isKinematic = true;
		myRigidbody2D.velocity = new Vector3(0, 0, 0);
		audioSource.clip = playerCaughtClip;
		audioSource.Play();

		foreach (var animator in animators)
		{
			animator.SetBool("Caught", true);
		}
		
		yield return new WaitForSeconds(1f);
	}

	IEnumerator EatWorm() {
		foreach (var animator in animators)
		{
			animator.SetBool("EatWorm", true);
		}
		audioSource.Stop();
		audioSource.clip = null;
		audioSource.loop = false;
		audioSource.PlayOneShot(eatWormClip);

		yield return new WaitForSeconds(eatWormAnimClip.length);
		if (loadNextLevel) {
			Debug.Log("Move to next Level");
			LevelManager.instance.LoadLevel(LevelManager.WinLevelString);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Worm"))
		{
			loadNextLevel = true;
			eat = StartCoroutine(EatWorm());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Worm"))
		{
			foreach (var animator in animators)
			{
				animator.SetBool("EatWorm", false);
			}

			loadNextLevel = false;
			StopCoroutine(eat);
			Debug.Log("Stop Eating");

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			enemyAnimators = collision.GetComponentsInChildren<Animator>(true);
			foreach (var enemyAnimator in enemyAnimators)
			{
				if (enemyAnimator.isActiveAndEnabled)
				{
					enemyAnimator.SetBool("Catch", true);
				}
			}
			if (!isCaught) {
				StartCoroutine(PlayerCaught());
			}	
		}
	}

	private Vector3 MousePointerDirection()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z += Camera.main.nearClipPlane;

		Vector3 heading = mousePosition - transform.position;
		float distance = heading.magnitude - 5;
		Vector3 direction = heading / distance;
		return direction;
	}

	private void SetAnimations()
	{
		if (moveHorizontaly || moveVertically)
		{
			foreach (var animator in animators)
			{
				if (animator.isActiveAndEnabled)
				{
					animator.SetBool("Move", true);
				}
			}
		}
		else
		{
			foreach (var animator in animators)
			{
				if (animator.isActiveAndEnabled)
				{
					animator.SetBool("Move", false);
				}
			}
		}
	}

	private void FlipDirection()
	{
		if (moveHorizontaly ) {
			float DirectionX = Mathf.Sign(myRigidbody2D.velocity.x);
			if (DirectionX == 1) {
				chicken.localScale = new Vector2(.2f, .2f);
			}
			if (DirectionX == -1) {
				chicken.localScale = new Vector2(-.2f, .2f);
			}
		}

		if (moveVertically) {
			float DirectionY = Mathf.Sign(myRigidbody2D.velocity.y);
			if (DirectionY == 1) {
				rigFront.SetActive(false);
				rigBack.SetActive(true);
			}
			if (DirectionY == -1) {
				rigFront.SetActive(true);
				//foreach (var animator in animators)
				//{
				//	if (animator.isActiveAndEnabled)
				//	{
				//		animator.Play("normalState", 0, 0f);
				//	}
				//}	
				rigBack.SetActive(false);
			}
		}
	}
}
