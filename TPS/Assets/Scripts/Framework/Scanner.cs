using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour {
	[Range(0.2f, 2f)]
	[SerializeField]
	float scanSpeed;
	[Range(70.0f, 110.0f)]
	[SerializeField]
	float fieldOfView;
	[SerializeField]
	LayerMask mask;

	SphereCollider rangeTrigger;
	List<PlayerController> targets;
	PlayerController selectedTarget;

	private void Start() {
		targets = new List<PlayerController>();
		rangeTrigger = GetComponent<SphereCollider>();
		PrepareScan();
	}


	private void OnDrawGizmos() {
		Gizmos.color = Color.cyan;

		if (selectedTarget != null) {
			Gizmos.DrawLine(transform.position, selectedTarget.transform.position);
		}

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(this.fieldOfView / 2) * this.GetComponent<SphereCollider>().radius);
		Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent<SphereCollider>().radius);

	}

	Vector3 GetViewAngle(float angle) {
		float rad = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
	}

	void ScanForTargets() {
		Collider[] results = Physics.OverlapSphere(transform.position, rangeTrigger.radius);

		for (int i = 0; i < results.Length; i++) {
			var player = results[i].gameObject.GetComponent<PlayerController>();
			if (player == null || !IsInLineOfSight(Vector3.up, player.transform.position)) {
				continue;
			}

			targets.Add(player);

		}

		if (targets.Count == 1) {
			selectedTarget = targets[0];
		}
		else {
			float closest = rangeTrigger.radius;
			foreach (var target in targets) {
				if(Vector3.Distance(transform.position, target.transform.position) < closest) {
					closest = Vector3.Distance(transform.position, target.transform.position);
					selectedTarget = target;
				}
			}
		}

		PrepareScan();

	}

	bool IsInLineOfSight(Vector3 eyeHeight, Vector3 targetPosition) {
		Vector3 direction = targetPosition - transform.position;

		if (Vector3.Angle(transform.forward, direction.normalized) < fieldOfView / 2) {
			float distance = Vector3.Distance(transform.position, targetPosition);
			if (Physics.Raycast(transform.position + eyeHeight, direction.normalized, distance, mask)) {
				return false;
			}
			return true;
		}
		return false;
	}

	void PrepareScan() {
		if(selectedTarget != null) {
			return;
		}

		GameManager.Instance.Timer.Add(ScanForTargets, scanSpeed);
	}

}
