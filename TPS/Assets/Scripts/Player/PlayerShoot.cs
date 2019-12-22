using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	[SerializeField]
	Shooter assaultRifle;

	InputController inputController;

	void Awake() {
		inputController = GameManager.Instance.InputController;
	}

	void Update() {
		if(inputController.fire1) {
			assaultRifle.Fire();
		}
	}
}
