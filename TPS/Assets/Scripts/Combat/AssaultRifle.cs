using Framework;

public class AssaultRifle : Shooter {
	public override void Fire() {
		base.Fire();

		if(canFire) {
			//can
		}
	}

	public void Update() {
		if(GameManager.Instance.InputController.Reload) {
			WeaponReload();
		}
	}

}
