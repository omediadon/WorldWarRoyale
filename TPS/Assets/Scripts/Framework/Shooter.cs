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
	[SerializeField]
	Transform AimTarget;


	Transform muzzle;

	[HideInInspector]
	public WeaponReloader reloader;

	private ParticleSystem muzzleFire;

	void Awake() {
		muzzle = transform.Find("Model/Muzzle");

		reloader = GetComponent<WeaponReloader>();

		muzzleFire = muzzle.GetComponent<ParticleSystem>();
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

		muzzle.LookAt(AimTarget);

		fireEffect();

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

	private void fireEffect() {
		if (muzzleFire == null)
			return;

		muzzleFire.Play();
	}



}
