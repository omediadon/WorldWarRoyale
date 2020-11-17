using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController {
	bool playerAlive = true;

	private void Start() {
		GetComponent<Player>().PlayerHealth.OnDeath += this.PlayerHealth_OnDeath;
		GetComponent<Player>().PlayerHealth.OnReset += this.PlayerHealth_OnReset;
	}

	private void PlayerHealth_OnReset() {
		playerAlive = true;
	}

	private void PlayerHealth_OnDeath() {
		playerAlive = false;
	}

	void Update() {
		if(!playerAlive)
			return;

		if(inputController.MouseWheelUp) {
			SwitchWeapon(1);
		}
		if(inputController.MouseWheelDown) {
			SwitchWeapon(-1);
		}

		if(inputController.Fire1 && canFire) {
			ActiveShooter.Fire();
		}
	}
}
