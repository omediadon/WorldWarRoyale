﻿using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
public class EnemyAnimation : MonoBehaviour {
	[SerializeField]
	Animator animator = null;

	Pathfinder pathfinder;

	Vector3 lastPosition;

	private void Awake() {
		pathfinder = GetComponent<Pathfinder>();
	}

	private void Update() {
		float velocityZ = (transform.position - lastPosition).magnitude / Time.deltaTime;
		animator.SetFloat("Vertical", velocityZ / pathfinder.Agent.speed);
		if(velocityZ > 0) {
			animator.SetBool("IsWalking", true);
		}
		else {
			animator.SetBool("IsWalking", false);
		}

		lastPosition = transform.position;
	}
}
