using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Distructible : MonoBehaviour {
	[SerializeField]
	float hitPoints = 10;

	public event System.Action OnDeath;
	public event System.Action OnDamageRecieved;

	float damageTaken = 0;

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
		OnDeath?.Invoke();
	}

	public virtual void TakeDamege(float damage) {
		damageTaken += damage;

		OnDamageRecieved?.Invoke();

		if(HitPointsRemaining <= 0) {
			Die();
		}
	}

	public void Reset() {
		damageTaken = 0;
	}

}
