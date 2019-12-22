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

	int shotsFiredInClip;
	Guid containerItemId;

	private void Awake() {
		containerItemId = inventory.Add(this.name, maxAmmo);
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

		int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip );

		GameManager.Instance.Timer.Add(() => ExecuteReload(amountFromInventory), reloadTime);
		print("reloading");
	}

	private void ExecuteReload(int amount) {

		shotsFiredInClip -= amount;

		
		IsReloading = false;
		print("reloaded");
	}

	public void TakeFromClip(int rounds) {
		shotsFiredInClip += rounds;
	}
}
