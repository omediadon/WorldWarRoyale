using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter: MonoBehaviour {
	[SerializeField]
	Text text;

	PlayerShoot playerShoot;
	WeaponReloader weaponReloader;

	void Awake() {
		GameManager.Instance.OnLocalPlayerJoined += OnPlayerJoined;
	}

	 void OnPlayerJoined(PlayerController player) {
		playerShoot = player.PlayerShoot;
		weaponReloader = playerShoot.ActiveShooter.reloader;
		weaponReloader.OnAmmoChanged += this.Reloader_OnAmmoChanged;
		playerShoot.OnWeaponSwitch += this.PlayerShoot_OnWeaponSwitch;
	}

	private void PlayerShoot_OnWeaponSwitch() {
		weaponReloader = playerShoot.ActiveShooter.reloader;
		Reloader_OnAmmoChanged();
	}

	private void Reloader_OnAmmoChanged() {
		text.text = weaponReloader.RoundsRemainingInClip + "/" + weaponReloader.RoundsRemainingInInventory;
	}

	void Update() {

	}
}
