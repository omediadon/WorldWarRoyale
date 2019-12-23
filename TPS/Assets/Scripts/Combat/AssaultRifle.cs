using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Shooter
{
	public override void Fire() {
		base.Fire();

		if(canFire) {
			//can
		}
	}

	public void Update() {
		if(GameManager.Instance.InputController.reload) {
			WeaponReload();
		}
	}

}
