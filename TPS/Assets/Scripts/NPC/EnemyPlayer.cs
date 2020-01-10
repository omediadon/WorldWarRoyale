﻿using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(Scanner))]
public class EnemyPlayer : MonoBehaviour {
	Pathfinder pathfinder;
	Scanner playerScanner;

	Player priorityTarget;

	List<Player> targets;

	private void Start() {
		pathfinder = GetComponent<Pathfinder>();
		playerScanner = GetComponent<Scanner>();
		playerScanner.OnScanReady += this.Scanner_OnScanReady;
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
			SetDestination();
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


}