using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Distructible: MonoBehaviour {
	[SerializeField]
	float hitPoints;

	public event System.Action OnDeath;
	public event System.Action OnDamageRecieved;

	float damageTaken;

	public float HitPointsRemaining {
		get {
			return hitPoints - damageTaken;
		}
	}

	public bool IsAlive {
		get {
			return HitPointsRemaining > 0;
		}
	}

	public virtual void Die() {
		if(!IsAlive) {
			return;
		}

		if(OnDeath != null) {
			OnDeath();
		}
	}

	public virtual void TakeDamege(float damage) {
		damageTaken += damage;

		if(OnDamageRecieved != null) {
			OnDamageRecieved();
		}

		if(HitPointsRemaining <= 0) {
			Die();
		}
	}

	public void Reset() {
		damageTaken = 0;
	}

}
