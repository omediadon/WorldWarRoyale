using UnityEngine;

public class SpawnPoint : MonoBehaviour {
	private void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.zero + Vector3.up, Vector3.one + Vector3.up);
	}
}
