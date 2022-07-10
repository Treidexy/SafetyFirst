using UnityEngine;
using UnityEngine.InputSystem;

using NaughtyAttributes;

public class PlayerMovement: MonoBehaviour {
	[Required]
	public GroundCheck groundCheck;
	
	public Rigidbody rb;

	public float normalSize;
	public float normalDrag;
	public float normalSpeed;

	[Required]
	public PlayerInput input;

	[Header("Movement")]
	[ReadOnly]
	public Vector3 move;
	[ReadOnly]
	public float speed;

	[Header("Jumping")]
	public float jumpForce;
	[ReadOnly]
	public float timeSinceJumpQueued;
	public float jumpQueueTimeout;
	[ReadOnly]
	public bool jumping;

	[Header("Sliding")]
	[ReadOnly]
	public bool sliding;
	public float slideSize;
	public float slideDrag;
	public float slideSpeed;
	
	void OnValidate() {
		if (rb == null)
			rb = gameObject.AddComponent<Rigidbody>();
	}

	void Start() {
		normalSize = transform.localScale.y;
		normalDrag = rb.drag;
		speed = normalSpeed;

		input.actions["Player/Move"].canceled += ctx => move = Vector3.zero;
		input.actions["Player/Slide"].started += ctx => Slide();
		input.actions["Player/Slide"].canceled += ctx => EndSlide();
	}

	void FixedUpdate() {
		var moveInput = input.actions["Player/Move"].ReadValue<Vector2>();
		move = (moveInput.x * transform.right + moveInput.y * transform.forward) * speed;
		rb.AddForce(move * Time.deltaTime, ForceMode.Acceleration);

		timeSinceJumpQueued += Time.deltaTime;

		if (input.actions["Player/Jump"].IsPressed()) {
			TryJump();
		}

		if (groundCheck.grounded && timeSinceJumpQueued <= jumpQueueTimeout) {
			rb.AddForce(jumpForce * transform.up, ForceMode.Acceleration);
		}
	}

	public void TryJump() {
		timeSinceJumpQueued = 0;
	}

	public void Slide() {
		sliding = true;

		var scale = transform.localScale;
		scale.y = slideSize;
		transform.localScale = scale;
		rb.drag = slideDrag;

		speed = slideSpeed;
	}

	public void EndSlide() {
		sliding = false;

		var scale = transform.localScale;
		scale.y = normalSize;
		transform.localScale = scale;
		rb.drag = normalDrag;

		speed = normalSpeed;
	}
}