﻿using UnityEngine;

public class PlayerStates : MonoBehaviour {
	public enum EMoveState {
		WALKING,
		RUNNING,
		SPRINTING,
		CROUCHING,
		COVER
	}

	public enum EWeaponState {
		AIMING,
		FIRING,
		AIMINGFIRING,
		IDLE
	}

	public EMoveState MoveState;
	public EWeaponState WeaponState;

	private InputController m_InputController;
	private bool  isInCover = false;

	public InputController InputController {
		get {
			if(m_InputController == null)
				m_InputController = GameManager.Instance.InputController;
			return this.m_InputController;
		}
	}

	private void Update() {
		SetMoveState();
		SetWeaponState();
	}

	void SetWeaponState() {
		WeaponState = EWeaponState.IDLE;

		if(InputController.Fire1) {
			WeaponState = EWeaponState.FIRING;
		}
		if(InputController.Fire2) {
			WeaponState = EWeaponState.AIMING;
		}
		if(InputController.Fire1 && InputController.Fire2) {
			WeaponState = EWeaponState.AIMINGFIRING;
		}

	}
	void SetMoveState() {
		MoveState = EMoveState.RUNNING;

		if(InputController.IsSprinting) {
			MoveState = EMoveState.SPRINTING;
		}

		if(!InputController.IsSprinting) {
			MoveState = EMoveState.WALKING;
		}

		if(InputController.IsCrouching) {
			MoveState = EMoveState.CROUCHING;
		}

		if(isInCover) {
			MoveState = EMoveState.COVER;
		}
	}

	private void Awake() {
		GameManager.Instance.EventBus.AddListener("CoverToggle", new EventBus.EventListener() {
			Method = toggleCover,
			IsSingleShot = false
		});
	}

	void toggleCover() {
		isInCover = !isInCover;
	}
}
