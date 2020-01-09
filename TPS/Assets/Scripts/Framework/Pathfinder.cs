using System;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinder : MonoBehaviour {
	[HideInInspector]
	public NavMeshAgent Agent;

	[SerializeField]
	float remainingDistThreshold = 1;

	public event Action OnDestinationReached;

	bool m_DestinationReached;
	bool DestinationReached {
		get {
			return this.m_DestinationReached;
		}
		set {
			this.m_DestinationReached = value;
			if (value == true) {
				OnDestinationReached?.Invoke();
			}
		}
	}

	private void Start() {
		Agent = GetComponent<NavMeshAgent>();
	}

	public void SetTarget(Vector3 target) {
		Agent.SetDestination(target);
	}

	private void Update() {
		if (DestinationReached == true) {
			return;
		}

		if (Agent.remainingDistance < remainingDistThreshold) {
			DestinationReached = true;
		}
	}
}
