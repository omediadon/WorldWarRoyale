using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(Scanner))]
public class EnemyPlayer : MonoBehaviour {
	Pathfinder pathfinder;
	Scanner scanner;
	[SerializeField]
	Animator animator = null;

	private void Start() {
		pathfinder = GetComponent<Pathfinder>();
		scanner = GetComponent<Scanner>();
		scanner.OnTargetSelected += this.Scanner_OnTargetSelected;
		if (animator == null) {
			animator = GetComponentInChildren<Animator>();
		}
	}

	private void Scanner_OnTargetSelected(Vector3 position) {
		pathfinder.SetTarget(position);
	}

	private void Update() {
		if (animator != null) {
			animator.SetFloat("Vertical", pathfinder.Agent.velocity.z);
			animator.SetFloat("Horizontal", pathfinder.Agent.velocity.x);
		}
	}
}
