using UnityEngine;

public class Entity: MonoBehaviour {
	public HpBar hpBar;
	public float maxHealth;
	public float health;

	void OnValidate() {
		health = maxHealth;
	}

	public void Damage(float amt) {
		health -= amt;
		if (health <= 0) {
			Destroy(gameObject);
		}

		hpBar.bar.localScale = new Vector3(1, 1, health / maxHealth);
		hpBar.bar.localPosition = new Vector3(0, 0, -5 * (health / maxHealth) + 5);
	}
}
