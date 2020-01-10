using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(Scanner))]
public class EnemyPlayer : MonoBehaviour {
	Pathfinder pathfinder;
	Scanner scanner;

	private void Start() {
		pathfinder = GetComponent<Pathfinder>();
		scanner = GetComponent<Scanner>();
		scanner.OnTargetSelected += this.Scanner_OnTargetSelected;
	}

	private void Scanner_OnTargetSelected(Vector3 position) {
		pathfinder.SetTarget(position);
	}


}
