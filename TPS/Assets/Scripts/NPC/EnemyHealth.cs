public class EnemyHealth : Distructible {
	RagDoll ragDoll;

	private void Start() {
		ragDoll = GetComponentInChildren<RagDoll>();
	}

	public override void Die() {
		base.Die();
		ragDoll.EnableRagdoll(true);
	}
}
