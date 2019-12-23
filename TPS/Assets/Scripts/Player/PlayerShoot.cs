using UnityEngine;
using System;

public class PlayerShoot: MonoBehaviour {
	[SerializeField]
	float weaponSwitchTime;

	bool canSwitch = true;
	bool canFire = true;

	Transform weaponHolder;

	[SerializeField]
	Shooter[] weapons;
	[SerializeField]
	private Shooter activeWeapon;
	[SerializeField]
	int currentWeaponIndex = 0;

	InputController inputController;
	Timer timer;

	public event Action OnWeaponSwitch;

	public Shooter ActiveShooter {
		get {
			return activeWeapon;
		}
	}

	void Awake() {
		timer = GameManager.Instance.Timer;
		inputController = GameManager.Instance.InputController;
		weaponHolder = transform.Find("Weapons");
		weapons = weaponHolder.GetComponentsInChildren<Shooter>();
		if(weapons.Length > 0) {
			DeactivateWeapons();
			Equip(currentWeaponIndex);
		}
	}

	void SwitchWeapon(int direction) {
		int old = currentWeaponIndex;
		canFire = false;
		currentWeaponIndex += direction;
		if(currentWeaponIndex > weapons.Length - 1) {
			currentWeaponIndex = 0;
		}
		if(currentWeaponIndex < 0) {
			currentWeaponIndex = weapons.Length - 1;
		}

		timer.Add(() => {
			weapons[old].gameObject.SetActive(false);
			weapons[old].transform.parent = weaponHolder;
			Equip(currentWeaponIndex);

		}, weaponSwitchTime);
	}

	void Equip(int index) {
		activeWeapon = weapons[index];
		weapons[index].gameObject.SetActive(true);
		weapons[index].Equip();
		canFire = true;
		if(OnWeaponSwitch != null)
			OnWeaponSwitch();
	}

	void Update() {
		if(inputController.MouseWheelUp) {
			SwitchWeapon(1);
		}
		if(inputController.MouseWheelDown) {
			SwitchWeapon(-1);
		}

		if(inputController.Fire1 && canFire) {
			activeWeapon.Fire();
		}
	}

	void DeactivateWeapons() {
		foreach(Shooter weapon in weapons) {
			weapon.gameObject.SetActive(false);
		}
	}
}
