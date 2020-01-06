using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	[SerializeField]
	float speed;
	[SerializeField]
	float timeToLive;
	[Tooltip("Damage value between 0 and 100.")]
	[SerializeField]
	float damage;

	void Start() {
		Destroy(gameObject, timeToLive);
	}

	void Update() {
		float distance = Time.deltaTime * speed;
		transform.Translate(Vector3.forward * distance);

		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, 5f)) {
			checkDistructible(hit.transform);
		}
	}

	void checkDistructible(Transform other) {

		var distructible = other.GetComponent<Distructible>();
		if(distructible == null) {
			return;
		}
		distructible.TakeDamege(damage);
		Destroy(this,0);
	}
}
