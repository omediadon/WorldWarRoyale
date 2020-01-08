using System;

using UnityEngine;

public class PlayerHealth : Distructible {
	[SerializeField] SpawnPoint[] spawnPoints = Array.Empty<SpawnPoint>();

	public override void Die() {
		base.Die();
		SpawnAtNew();
	}

	void SpawnAtNew() {
		int spawnIdx = UnityEngine.Random.Range(0, spawnPoints.Length);

		transform.position = spawnPoints[spawnIdx].transform.position;
		transform.rotation = spawnPoints[spawnIdx].transform.rotation;
	}

	[ContextMenu("die")]
#pragma warning disable IDE0051
	void TestDie() {
#pragma warning restore IDE0051
		Die();
	}
}
