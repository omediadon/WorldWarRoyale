using UnityEngine;

public class AmmoPickup : PickupItem {

	[SerializeField]
	EWeaponType weaponType = EWeaponType.SCARH;
	[SerializeField]
	float respawnTime = 1;
	[SerializeField]
	int amount = 30;

	public override void OnPickUpItem(Transform item) {
		var playerInventory = item.GetComponentInChildren<Container>();
		GameManager.Instance.Respawner.Despawn(this.gameObject, respawnTime);
		playerInventory.Put(weaponType.ToString(), amount);

		//TODO: Check if has no reloader
		item.GetComponent<Player>().PlayerShoot.ActiveShooter.reloader.HandleAmmoChange();
	}
}
