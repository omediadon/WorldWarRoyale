using System;
using UnityEngine;

public class WeaponReloader: MonoBehaviour {
	[SerializeField]
	int clipSize;
	[SerializeField]
	float reloadTime;
	[SerializeField]
	int maxAmmo;
	[SerializeField]
	Container inventory;

	[SerializeField]
	int shotsFiredInClip;

	Guid containerItemId;

	private void Awake() {
		GameManager.Instance.Timer.Add(() => containerItemId = inventory.Add(this.name, maxAmmo), 0.01f);
	}

	public int RoundsRemainingInClip {
		get {
			return clipSize - shotsFiredInClip;
		}
	}

	public bool IsReloading {
		get;
		private set;
	}

	public void Reload() {
		if(IsReloading) {
			return;
		}

		IsReloading = true;

		print("round in this clip: " + RoundsRemainingInClip);
		int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip );

		if(amountFromInventory > -0) {
			GameManager.Instance.Timer.Add(() => ExecuteReload(amountFromInventory), reloadTime);
		}
		else {
			IsReloading = false;
			print("inventory empty");
		}

	}

	private void ExecuteReload(int amount) {

		shotsFiredInClip -= amount;
		print("amount added: " + amount);

		IsReloading = false;
	}

	public void TakeFromClip(int rounds) {
		shotsFiredInClip += rounds;
	}
}
