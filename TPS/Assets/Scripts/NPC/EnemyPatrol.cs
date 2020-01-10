using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
public class EnemyPatrol : MonoBehaviour {
	[SerializeField]
	private WayPointController wayPointController = null;

	[Range(0f, 3f)]
	[SerializeField]
	float waitTimeMin = 1f;
	[Range(2f, 5f)]
	[SerializeField]
	float waitTimeMax = 3f;

	private Pathfinder pathfinder;

	private void Awake() {
		pathfinder = GetComponent<Pathfinder>();
		pathfinder.OnDestinationReached += this.Pathfinder_OnDestinationReached;
		wayPointController.OnWayPointChanged += this.WayPointController_OnWayPointChanged;
	}

	private void WayPointController_OnWayPointChanged(WayPoint wayPoint) {
		pathfinder.SetTarget(wayPoint.transform.position);
		pathfinder.DestinationReached = false;
	}

	private void Pathfinder_OnDestinationReached() {
		GameManager.Instance.Timer.Add(wayPointController.SetNextWayPoint, Random.Range(waitTimeMin, waitTimeMax));
	}
}
