using UnityEngine;
using UnityEngine.InputSystem;

using NaughtyAttributes;

public class PlayerAttack: MonoBehaviour {
	public Rigidbody rb;

	[Required]
	public PlayerInput input;

	[Required]
	public ParticleSystem critParticle;
	[ReadOnly]
	public ParticleSystemRenderer critParticleRenderer;

	[Header("Technical")]
	public Transform rayOrigin;
	public LayerMask mask;
	public float reach;

	public float critThreshold;
	[ReadOnly]
	public bool criting;

	[Header("Attack")]
	public float damage;
	public float critBonus;

	void OnValidate() {
		if (rayOrigin == null) {
			rayOrigin = transform;
		}

		if (rb == null) {
			rb = GetComponent<Rigidbody>();
		}

		critParticleRenderer = critParticle.GetComponent<ParticleSystemRenderer>();
	}

	void Start() {
		input.actions["Player/Attack"].performed += ctx => Attack();
	}

	void FixedUpdate() {
		criting = rb.velocity.y < critThreshold;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * reach);
	}

	public void Attack() {
		if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, reach, mask)) {
			float dmg = damage;
			
			if (criting) {
				dmg += critBonus;
				critParticle.transform.position = hit.point;
				critParticleRenderer.material = hit.collider.GetComponentInChildren<Renderer>().material;
				critParticle.Play();
			}

			hit.collider.GetComponent<Entity>().Damage(dmg);
		}
	}
}