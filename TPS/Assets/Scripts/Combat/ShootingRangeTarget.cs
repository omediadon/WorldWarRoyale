﻿using UnityEngine;

public class ShootingRangeTarget : Distructible {
	[SerializeField] float rotationSpeed = 1;
	[SerializeField] float repairTime = 3;

	Quaternion initRotation = Quaternion.identity;
	Quaternion targRotation = Quaternion.identity;
	bool requireRotation = false;

	// Start is called before the first frame update
	void Awake() {
		initRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update() {
		if(!requireRotation) {
			return;
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, targRotation, rotationSpeed * Time.deltaTime);
		if(transform.rotation == targRotation) {
			requireRotation = false;
			Reset();
		}
	}

	public override void Die() {
		base.Die();
		targRotation = Quaternion.Euler(transform.right * 90);
		requireRotation = true;

		GameManager.Instance.Timer.Add(() => {
			targRotation = initRotation;
			requireRotation = true;
		}, repairTime);

	}
}
