using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	Animator animator;
	InputController inputController;

	private void Awake() {
		animator = GetComponentInChildren<Animator>();
		inputController = GameManager.Instance.InputController;
	}

	private void Update() {
		animator.SetFloat("Vertical", inputController.vertical);
		animator.SetFloat("Horizontal", inputController.horizontal);

		animator.SetBool("IsWalking", !inputController.IsSprinting);
	}
}
