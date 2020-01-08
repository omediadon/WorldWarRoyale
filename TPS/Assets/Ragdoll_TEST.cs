using UnityEngine;

public class Ragdoll_TEST : Distructible {
	Rigidbody[] bodyParts;
	Animator animator;
	MoveController moveController;

	private void Start() {
		bodyParts = transform.GetComponentsInChildren<Rigidbody>();
		animator = transform.GetComponent<Animator>();
		moveController = transform.GetComponent<MoveController>();
		SetRagdoll(false);
	}

	private void Update() {
		if (animator.enabled) {
			animator.SetBool("IsWalking", true);
			animator.SetFloat("Vertical", 1f);
			moveController.move(new Vector2(3, 1));
		}
	}

	void SetRagdoll(bool enable) {
		for (int i = 0; i < bodyParts.Length; i++) {
			bodyParts[i].isKinematic = !enable;
		}
	}

	public override void Die() {
		base.Die();
		SetRagdoll(true);
		animator.enabled = false;
	}
}
