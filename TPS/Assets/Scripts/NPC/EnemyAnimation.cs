using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
public class EnemyAnimation : MonoBehaviour {
	[SerializeField]
	private Animator animator = null;

	private Vector3 lastPosition;
	private Pathfinder pathfinder;


	private void Awake() {
		pathfinder = GetComponent<Pathfinder>();
	}


	private void Update() {
		float velocityZ = (transform.position - lastPosition).magnitude / Time.deltaTime;
		animator.SetFloat("Vertical", velocityZ / pathfinder.Agent.speed);
		animator.SetBool("IsWalking", true);

		lastPosition = transform.position;
	}
}
