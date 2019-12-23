using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem
{

	[SerializeField]
	EWeaponType weaponType;
	[SerializeField]
	float respawnTime;
	[SerializeField]
	int amount;

	public override void OnPickUpItem(Transform item) {
		var playerInventory = item.GetComponentInChildren<Container>();
		GameManager.Instance.Respawner.Despawn(this.gameObject, respawnTime);
		playerInventory.Put(weaponType.ToString(), amount);

		//TODO: Check if has no reloader
		item.GetComponent<PlayerController>().PlayerShoot.ActiveShooter.reloader.HandleAmmoChange();
	}
}
