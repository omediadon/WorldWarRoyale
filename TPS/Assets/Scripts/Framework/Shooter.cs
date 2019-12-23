using UnityEngine;

public class Shooter: MonoBehaviour {
	[SerializeField]
	float rateOfFire;
	[SerializeField]
	Projectile projectile;

	[HideInInspector]
	public float nextFireAllowed;
	[HideInInspector]
	public bool canFire;

	[SerializeField]
	Transform Hand;


	Transform muzzle;

	public WeaponReloader reloader;

	void Awake() {
		muzzle = transform.Find("Model/Muzzle");

		reloader = GetComponent<WeaponReloader>();
	}

	public virtual void Fire() {
		canFire = false;

		if(Time.time < nextFireAllowed)
			return;

		if(reloader != null) {
			if(reloader.IsReloading) {
				return;
			}

			if(reloader.RoundsRemainingInClip == 0) {
				return;
			}

			reloader.TakeFromClip(1);
		}

		nextFireAllowed = Time.time + rateOfFire;

		Instantiate(projectile, muzzle.position, muzzle.rotation);

		canFire = true;
	}


	public void WeaponReload() {
		if(reloader == null) {
			return;
		}

		reloader.Reload();
	}

	public void Equip() {
		transform.SetParent(Hand);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}



}
