using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour {
	[SerializeField]
	private Animator animator = null;

	private Vector3 lastPosition;
	private Pathfinder pathfinder;
	EnemyPlayer enemyPlayer;

	private void Awake() {
		pathfinder = GetComponent<Pathfinder>();
		enemyPlayer = GetComponent<EnemyPlayer>();
	}

	private void Update() {
		float velocityZ = (transform.position - lastPosition).magnitude / Time.deltaTime;
		animator.SetFloat("Vertical", velocityZ / pathfinder.Agent.speed);
		animator.SetBool("IsWalking", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE);

		lastPosition = transform.position;
	}
}
