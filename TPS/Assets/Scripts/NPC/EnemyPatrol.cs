using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour {
	[SerializeField]
	private WayPointController wayPointController = null;

	[Range(0f, 3f)]
	[SerializeField]
	float waitTimeMin = 1f;
	[Range(2f, 5f)]
	[SerializeField]
	float waitTimeMax = 3f;

	EnemyPlayer m_EnemyPlayer;
	public EnemyPlayer EnemyPlayer {
		get {
			if(m_EnemyPlayer == null) {
				m_EnemyPlayer = GetComponent<EnemyPlayer>();
			}
			return m_EnemyPlayer;
		}
	}

	private Pathfinder pathfinder;

	private void Start() {
	}

	private void Awake() {
		m_EnemyPlayer = GetComponent<EnemyPlayer>();
		pathfinder = GetComponent<Pathfinder>();
		pathfinder.OnDestinationReached += this.Pathfinder_OnDestinationReached;
		wayPointController.OnWayPointChanged += this.WayPointController_OnWayPointChanged;
		EnemyPlayer.EnemyHealth.OnDeath += this.EnemyHealth_OnDeath;
		EnemyPlayer.OnTargetSelected += this.EnemyPlayer_OnTargetSelected;
	}

	private void EnemyPlayer_OnTargetSelected(Player obj) {
		pathfinder.Agent.isStopped = true;
	}

	private void EnemyHealth_OnDeath() {
		pathfinder.Agent.isStopped = true;
	}

	private void WayPointController_OnWayPointChanged(WayPoint wayPoint) {
		pathfinder.SetTarget(wayPoint.transform.position);
		pathfinder.DestinationReached = false;
	}

	private void Pathfinder_OnDestinationReached() {
		GameManager.Instance.Timer.Add(wayPointController.SetNextWayPoint, Random.Range(waitTimeMin, waitTimeMax));
	}
}
