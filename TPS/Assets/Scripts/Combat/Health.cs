using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Distructible
{
	[SerializeField]
	private float inSeconds;

	public override void Die() {
		base.Die();

		print("dead!");

		GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
	}

	private void OnEnable() {
		Reset();
	}

	public override void TakeDamege(float damage) {
		base.TakeDamege(damage);

		print("remains: " + HitPointsRemaining);
	}
}
