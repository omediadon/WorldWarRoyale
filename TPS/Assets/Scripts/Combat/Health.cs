using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Distructible
{
	[SerializeField]
	private float inSeconds;

	public override void Die() {
		base.Die();
		
		GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
	}

	private void OnEnable() {
		Reset();
	}

	public override void TakeDamege(float damage) {
		base.TakeDamege(damage);
	}
}
