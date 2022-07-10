using UnityEngine;
using UnityEngine.AI;

using NaughtyAttributes;

public class Bandit: MonoBehaviour {
	[Required]
	public Transform baseAttract;
	[Required]
	public NavMeshAgent agent;

	public float stopDist;

	void Start() {
		agent.destination = baseAttract.position;
	}

	void FixedUpdate() {
		if (Vector3.Distance(transform.position, baseAttract.position) < stopDist) {
			print("bandit attack base");
			Destroy(gameObject);
		}
	}
}