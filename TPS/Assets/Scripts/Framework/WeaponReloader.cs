﻿using System;
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

	public event Action OnAmmoChanged;

	private void Awake() {
		GameManager.Instance.Timer.Add(() => containerItemId = inventory.Add(this.name, maxAmmo), 0.01f);
	}

	public int RoundsRemainingInClip {
		get {
			return clipSize - shotsFiredInClip;
		}
	}

	public int RoundsRemainingInInventory {
		get {
			return inventory.LeftInInventory(containerItemId);
			;
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

		if(amountFromInventory > -0) {
			GameManager.Instance.Timer.Add(() => ExecuteReload(amountFromInventory), reloadTime);
		}
		else {
			IsReloading = false;
		}

	}

	private void ExecuteReload(int amount) {

		shotsFiredInClip -= amount;

		IsReloading = false;
		if(OnAmmoChanged != null) {
			OnAmmoChanged();
		}
	}

	public void TakeFromClip(int rounds) {
		shotsFiredInClip += rounds;
		if(OnAmmoChanged != null) {
			OnAmmoChanged();
		}
	}
}
