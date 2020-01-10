using System;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinder : MonoBehaviour {
	NavMeshAgent m_Agent;
	[HideInInspector]
	public NavMeshAgent Agent {
		get {
			return this.m_Agent;
		}
		set {
			this.m_Agent = value;
			if(value != null) {
				OnAgentInit?.Invoke();
			}
		}
	}

	[SerializeField]
	float remainingDistThreshold = 1;

	public event Action OnDestinationReached;
	public event Action OnAgentInit;

	bool m_DestinationReached;
	public bool DestinationReached {
		get {
			return this.m_DestinationReached;
		}
		set {
			this.m_DestinationReached = value;
			if(value == true) {
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
		if(DestinationReached == true && !Agent.hasPath) {
			return;
		}

		if(Agent.remainingDistance <= remainingDistThreshold) {
			DestinationReached = true;
		}
	}
}
