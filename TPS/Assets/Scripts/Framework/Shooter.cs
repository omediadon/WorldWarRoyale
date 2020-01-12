using UnityEngine;

public class Shooter : MonoBehaviour {
	[SerializeField]
	float rateOfFire = 1000;
	[SerializeField]
	Projectile projectile = null;

	[HideInInspector]
	public float nextFireAllowed = 0;
	[HideInInspector]
	public bool canFire = true;

	[SerializeField]
	Transform Hand = null;
	[SerializeField]
	public Transform AimTarget = null;
	public Vector3 AimTargetOffset = Vector3.zero;

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

		if (Time.time < nextFireAllowed)
			return;

		if (reloader != null) {
			if (reloader.IsReloading) {
				return;
			}

			if (reloader.RoundsRemainingInClip == 0) {
				return;
			}

			reloader.TakeFromClip(1);
		}

		nextFireAllowed = Time.time + rateOfFire;

		if (AimTarget != null) {
			muzzle.LookAt(AimTarget.position + AimTargetOffset);
		}

		FireEffect();

		if (projectile != null) {
			Instantiate(projectile, muzzle.position, muzzle.rotation);
		}

		canFire = true;
	}


	public void WeaponReload() {
		if (reloader == null) {
			return;
		}

		reloader.Reload();
	}

	public void Equip() {
		transform.SetParent(Hand);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}

	private void FireEffect() {
		if (muzzleFire == null)
			return;

		muzzleFire.Play();
	}



}
