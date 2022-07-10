using UnityEngine;

public class GroundCheck: MonoBehaviour {
	public LayerMask groundLayers;
	public bool grounded;
	public float depth = 0.01f;
	
	void FixedUpdate() {
		grounded = Physics.CheckSphere(transform.position, depth, groundLayers.value);
	}

	void OnDrawGizmos() {
		Gizmos.color = grounded ? Color.green : Color.red;
		Gizmos.DrawWireSphere(transform.position, depth);
	}
}
