using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {
	[SerializeField]
	Text text = null;

	PlayerShoot playerShoot;
	WeaponReloader weaponReloader;

	void Awake() {
		GameManager.Instance.OnLocalPlayerJoinedX += OnPlayerJoined;
	}

	void OnPlayerJoined(Player player) {
		playerShoot = player.PlayerShoot;
		weaponReloader = playerShoot.ActiveShooter.reloader;
		weaponReloader.OnAmmoChanged += this.Reloader_OnAmmoChanged;
		playerShoot.OnWeaponSwitch += this.PlayerShoot_OnWeaponSwitch;
		Reloader_OnAmmoChanged();
	}

	private void PlayerShoot_OnWeaponSwitch() {
		weaponReloader = playerShoot.ActiveShooter.reloader;
		weaponReloader.OnAmmoChanged += this.Reloader_OnAmmoChanged;
		Reloader_OnAmmoChanged();
	}

	private void Reloader_OnAmmoChanged() {
		if (text != null) {
			text.text = weaponReloader.RoundsRemainingInClip + "/" + weaponReloader.RoundsRemainingInInventory;
		}
	}

}
