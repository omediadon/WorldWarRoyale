using UnityEngine;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController {
	[SerializeField]
	bool shouldShoot = false;

	[Range(1f, 5f)]
	[SerializeField]
	float shootSpeed = .1f;

	[Range(0.5f, 2f)]
	[SerializeField]
	float burstDurationMin = .5f;

	[Range(0.5f, 3f)]
	[SerializeField]
	float burstDurationMax = 2f;

	EnemyPlayer enemyPlayer;
	bool shootfire;

	private void Start() {
		enemyPlayer = GetComponent<EnemyPlayer>();
		if(shouldShoot)
			enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
	}

	private void EnemyPlayer_OnTargetSelected(Player target) {
		ActiveShooter.AimTarget = target.transform;
		ActiveShooter.AimTargetOffset = Vector3.up * 1.4f;
		StartBurst();
	}

	void StartBurst() {
		if(!enemyPlayer.EnemyHealth.IsAlive) {
			return;
		}

		CheckReload();

		shootfire = true;

		GameManager.Instance.Timer.Add(EndBurst, Random.Range(burstDurationMin, burstDurationMax));
	}


	void EndBurst() {
		shootfire = false;
		if(!enemyPlayer.EnemyHealth.IsAlive) {
			return;
		}

		GameManager.Instance.Timer.Add(StartBurst, shootSpeed);
	}

	void CheckReload() {
		if(ActiveShooter.reloader.RoundsRemainingInClip == 0) {
			ActiveShooter.reloader.Reload();
		}
	}

	private void Update() {
		if(!shootfire || !canFire || !enemyPlayer.EnemyHealth.IsAlive) {
			return;
		}

		ActiveShooter.Fire();
	}
}
