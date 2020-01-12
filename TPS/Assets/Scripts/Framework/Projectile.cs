using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
	[SerializeField]
	float speed = 35;
	[SerializeField]
	float timeToLive = 10;
	[Tooltip("Damage value between 0 and 100.")]
	[SerializeField]
	float damage = 1;

	void Start() {
		Destroy(gameObject, timeToLive);
	}

	void Update() {
		float distance = Time.deltaTime * speed;
		transform.Translate(Vector3.forward * distance);

		RaycastHit hit;

		if(Physics.Raycast(transform.position, transform.forward, out hit, 5f)) {
			CheckDistructible(hit.transform);
		}
	}

	void CheckDistructible(Transform other) {

		var distructible = other.GetComponent<Distructible>();
		if(distructible == null) {
			return;
		}
		distructible.TakeDamege(damage);
		Destroy(this, 0);
	}
}
