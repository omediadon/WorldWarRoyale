using System;

using UnityEngine;

public class WayPointController : MonoBehaviour {
	WayPoint[] wayPoints = Array.Empty<WayPoint>();

	int currentWayPoint = -1;

	public event Action<WayPoint> OnWayPointChanged;

	private void Awake() {
		wayPoints = GetWayPoints();
	}

	public void SetNextWayPoint() {
		currentWayPoint++;
		if(currentWayPoint == wayPoints.Length) {
			currentWayPoint = 0;
		}

		OnWayPointChanged?.Invoke(wayPoints[currentWayPoint]);
	}

	WayPoint[] GetWayPoints() {
		return GetComponentsInChildren<WayPoint>();
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.blue;

		Vector3 previousWayPoint = Vector3.zero;

		foreach(var wayPoint in GetWayPoints()) {
			Vector3 currentWayPoint = wayPoint.transform.position;
			Gizmos.DrawSphere(currentWayPoint, .1f);
			if(previousWayPoint != Vector3.zero) {
				Gizmos.DrawLine(previousWayPoint, currentWayPoint);
			}
			previousWayPoint = currentWayPoint;
		}
	}
}
