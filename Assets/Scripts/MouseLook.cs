using UnityEngine;
using UnityEngine.InputSystem;

using NaughtyAttributes;

public class MouseLook: MonoBehaviour {
	[Required]
	public PlayerInput input;
	public float sensitiviy = 100;
	public Transform body;

	public Vector2 xRotBounds;
	public float xRot = 0;

	public Vector2 look;

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;

		input.actions["Player/Look"].performed += ctx => look = ctx.ReadValue<Vector2>();
	}

	void Update() {
	#if !UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
		}
	#endif

		xRot -= look.y;
		xRot = Mathf.Clamp(xRot, xRotBounds.x, xRotBounds.y);
		
		transform.localRotation = Quaternion.Euler(xRot, 0, 0);
		body.Rotate(Vector3.up * look.x);

		look = Vector2.zero;
	}
}