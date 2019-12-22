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
	}

	void OnTriggerEnter(Collider other) {

		var distructible = other.transform.GetComponent<Distructible>();
		if(distructible == null) {
			return;
		}
		print("hit! " + other.name);
		distructible.TakeDamege(damage);
	}
}
