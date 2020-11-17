using System;

using UnityEngine;

public class PlayerHealth : Distructible {
	[SerializeField]
	SpawnPoint[] spawnPoints = Array.Empty<SpawnPoint>();

	RagDoll ragDoll;

	private void Start() {
		ragDoll = GetComponentInChildren<RagDoll>();
	}

	public override void Die() {
		base.Die();
		ragDoll.EnableRagdoll(true);
		GameManager.Instance.Timer.Add(SpawnAtNew, 2);
	}

	void SpawnAtNew() {
		Reset();
		ragDoll.EnableRagdoll(false);
		int spawnIdx = UnityEngine.Random.Range(0, spawnPoints.Length);

		transform.position = spawnPoints[spawnIdx].transform.position;
		transform.rotation = spawnPoints[spawnIdx].transform.rotation;
	}
}
