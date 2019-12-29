using UnityEngine;

public class PlayerAnimation: MonoBehaviour {
	Animator animator;
	InputController inputController;

	private PlayerAim m_PlayerAim;

	public PlayerAim PlayerAim {
		get {
			if(this.m_PlayerAim == null) {
				m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
			}
			return this.m_PlayerAim;
		}

		set {
			this.m_PlayerAim = value;
		}
	}

	private void Awake() {
		animator = GetComponentInChildren<Animator>();
		inputController = GameManager.Instance.InputController;
	}

	private void Update() {
		animator.SetFloat("Vertical", inputController.vertical);
		animator.SetFloat("Horizontal", inputController.horizontal);

		animator.SetBool("IsWalking", !inputController.IsSprinting);

		animator.SetFloat("AimAngle", PlayerAim.GetAngle());
	}
}
