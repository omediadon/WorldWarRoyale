using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
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

	private PlayerStates m_PlayerState;

	public PlayerStates PlayerState {
		get {
			if(this.m_PlayerAim == null) {
				m_PlayerState = GameManager.Instance.LocalPlayer.PlayerState;
			}
			return this.m_PlayerState;
		}

		set {
			this.m_PlayerState = value;
		}
	}



	private void Awake() {
		animator = GetComponentInChildren<Animator>();
		GameManager.Instance.OnLocalPlayerJoinedY += this.Instance_OnLocalPlayerJoinedY;
		inputController = GameManager.Instance.InputController;
		
	}

	private void Instance_OnLocalPlayerJoinedY(Player obj) {
		PlayerState = GameManager.Instance.LocalPlayer.PlayerState;
	}

	private void Update() {
		animator.SetFloat("Vertical", inputController.vertical);
		animator.SetFloat("Horizontal", inputController.horizontal);

		animator.SetBool("IsWalking", !inputController.IsSprinting);

		animator.SetFloat("AimAngle", PlayerAim.GetAngle());
		if(PlayerState != null) {
			animator.SetBool("IsInCover", PlayerState.MoveState == PlayerStates.EMoveState.COVER);
		}
		
	}
}
