using UnityEngine;

public class RagDoll : MonoBehaviour {
	Animator Animator;
	Rigidbody[] bodyParts;


	private void Awake() {
		bodyParts = transform.GetComponentsInChildren<Rigidbody>();
		Animator = transform.GetComponent<Animator>();
		EnableRagdoll(false);
	}

	private void Update() {

	}

	public void EnableRagdoll(bool enable) {
		Animator.enabled = !enable;
		for(int i = 0; i < bodyParts.Length; i++) {
			bodyParts[i].isKinematic = !enable;
		}
	}

}
