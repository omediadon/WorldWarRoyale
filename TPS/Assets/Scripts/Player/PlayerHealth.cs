using UnityEngine;

public class PlayerHealth : Distructible {
	[SerializeField] SpawnPoint[] spawnPoints;

	public override void Die() {
		base.Die();
		spawnAtNew();
	}

	void spawnAtNew() {
		int spawnIdx = Random.Range(0, spawnPoints.Length);

		transform.position = spawnPoints[spawnIdx].transform.position;
		transform.rotation = spawnPoints[spawnIdx].transform.rotation;
	}

	[ContextMenu("die")]
	void testDie() {
		Die();
	}
}
