using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : MonoBehaviour {
	[SerializeField]
	SoldierPro settings = null;

	Pathfinder pathfinder;
	Scanner playerScanner;

	Player priorityTarget;

	List<Player> targets;

	EnemyHealth m_EnemyHealth;
	public EnemyHealth EnemyHealth {
		get {
			if(m_EnemyHealth == null) {
				m_EnemyHealth = GetComponent<EnemyHealth>();
			}
			return m_EnemyHealth;
		}
	}


	public EnemyState EnemyState {
		get {
			if(_EnemyState == null) {
				_EnemyState = GetComponent<EnemyState>();
			}
			return _EnemyState;
		}
	}
	private EnemyState _EnemyState;



	public event Action<Player> OnTargetSelected;

	private void Awake() {
		pathfinder = GetComponent<Pathfinder>();
		pathfinder.OnAgentInit += () => {
			pathfinder.Agent.speed = settings.WalkSpeed;
		};
		playerScanner = GetComponent<Scanner>();
		if(playerScanner != null) {
			playerScanner.OnScanReady += this.Scanner_OnScanReady;
			Scanner_OnScanReady();
		}

		EnemyHealth.OnDeath += this.EnemyHealth_OnDeath;
		EnemyState.OnModeChanged += this.EnemyState_OnModeChanged;
	}

	private void EnemyState_OnModeChanged(EnemyState.EMode state) {
		if(pathfinder.Agent != null) {
			if(state == EnemyState.EMode.AWARE) {
				pathfinder.Agent.speed = settings.RunSpeed;
			}
			else {
				pathfinder.Agent.speed = settings.WalkSpeed;
			}
		}
	}

	private void EnemyHealth_OnDeath() {
	}

	private void Scanner_OnScanReady() {
		if(priorityTarget != null) {
			return;
		}

		targets = playerScanner.ScanForTargets<Player>();

		if(targets.Count == 1) {
			priorityTarget = targets[0];
		}
		else {
			SelectClosestTarget();
		}

		if(priorityTarget != null) {
			OnTargetSelected?.Invoke(priorityTarget);
		}
	}

	private void SelectClosestTarget() {
		float closest = playerScanner.ScanRange;
		foreach(var target in targets) {
			if(Vector3.Distance(transform.position, target.transform.position) <= closest) {
				closest = Vector3.Distance(transform.position, target.transform.position);
				priorityTarget = target;
			}
		}
	}

	private void SetDestination() {
		pathfinder.SetTarget(priorityTarget.transform.position);
	}

	private void Update() {
		if(priorityTarget == null) {
			return;
		}
		transform.LookAt(priorityTarget.transform);
	}


}
