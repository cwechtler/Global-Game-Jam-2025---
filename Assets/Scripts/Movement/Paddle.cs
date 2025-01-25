using System;
using UnityEngine;

public class Paddle : MonoBehaviour {
	[SerializeField] private float paddlespeed = 10f;
	[SerializeField] private int rotationDegrees = 30;
	[SerializeField] private Transform nozzle;
	[SerializeField] private Transform tail;
	[Space]
	[Header("Mouse Binding")]
	[SerializeField] private float screenLeftLimit = -8f;
	[SerializeField] private float screenRightLimit = 8f;

	private Rigidbody2D rb;
	private Camera mainCamera;

	private float screenHalfWidth;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		mainCamera = Camera.main;

		// Calculate width of the paddle in world units
		float paddleWidth = transform.localScale.x;
		// Get screen width in world units
		screenHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height - paddleWidth;

		if (GameController.instance.MouseControl) {
			Cursor.lockState = CursorLockMode.Confined;
		}
	}
		
	void Update () {
		if (GameController.instance.MouseControl){
			MoveWithMouse();
			Rotate(true);
		} else{
			Move();
			Rotate();
		}	
	}

	void MoveWithMouse(){
		// Get mouse position in world space
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, 0, -mainCamera.transform.position.z));

		// Clamp paddle movement within screen bounds
		float clampedX = Mathf.Clamp(mousePosition.x, screenLeftLimit, screenRightLimit);
		transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
	}
	void Move(){
		float inputX = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(inputX * paddlespeed, rb.velocity.y);
		
		// This allows for snapping back to the center. use instead of rb.velocity.
		//this.transform.position = new Vector3(inputX, this.transform.position.y, this.transform.position.z);

		// Clamp position
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenHalfWidth, screenHalfWidth);
		transform.position = clampedPosition;
	}

	void Rotate(bool invert = false) {
		float inputZ = Input.GetAxis(invert ? "Horizontal" : "Horizontal2");
		nozzle.localEulerAngles = new Vector3(0, 0, inputZ * rotationDegrees);
		tail.localEulerAngles = new Vector3(0, 0, inputZ * rotationDegrees);
	}
}
