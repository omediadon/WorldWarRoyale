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
	[SerializeField]
	Transform bulletHole = null;

	Vector3 destination = Vector3.zero;

	void Start() {
		Destroy(gameObject, timeToLive);
	}

	void Update() {
		if(IsDestinationReached()) {
			Destroy(gameObject, 0);
		}

		float distance = Time.deltaTime * speed;


		transform.Translate(Vector3.forward * distance);


		if(Vector3.zero != destination) {
			return;
		}

		RaycastHit hit;

		if(Physics.Raycast(transform.position, transform.forward, out hit, 5f)) {
			CheckDistructible(hit);
		}
	}

	void CheckDistructible(RaycastHit hit) {

		var distructible = hit.transform.GetComponent<Distructible>();

		destination = hit.point + hit.normal * 0.005f;

		Transform hole = Instantiate(bulletHole, destination, Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180, 0));

		hole.SetParent(hit.transform);

		if(distructible == null) {
			return;
		}
		distructible.TakeDamege(damage);
		Destroy(gameObject, 0);
	}

	bool IsDestinationReached() {
		if(destination == Vector3.zero) {
			return false;
		}

		Vector3 directionToDestination = destination - transform.position;
		float dot = Vector3.Dot(directionToDestination, transform.forward);
		if(dot > 0) {
			return false;
		}
		return true;
	}
}
