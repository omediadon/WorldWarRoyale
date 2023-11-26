using System;
using Framework;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	[Serializable]
	class CameraRig {
		public Vector3 cameraOffset = Vector3.zero;
		public float Damping = 0;
	}

	[SerializeField]
	CameraRig defaultCameraRig = null;
	[SerializeField]
	CameraRig aimCameraRig = null;

	Player localPlayer;
	Transform cameraLookTarget;

	// Start is called before the first frame update
	void Awake() {
		GameManager.Instance.OnLocalPlayerJoined += this.Instance_OnLocalPlayerJoined;
	}

	// Update is called once per frame
	void LateUpdate() {
		if(localPlayer == null) {
			return;
		}

		CameraRig currentCamera = defaultCameraRig;

		if(localPlayer.PlayerState.WeaponState == PlayerStates.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerStates.EWeaponState.AIMINGFIRING) {
			currentCamera = aimCameraRig;
		}

		Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * currentCamera.cameraOffset.z
			+ localPlayer.transform.up * currentCamera.cameraOffset.y
			+ localPlayer.transform.right * currentCamera.cameraOffset.x;

		Quaternion targetRotation = cameraLookTarget.rotation; //Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

		transform.position = Vector3.Lerp(transform.position, targetPosition, currentCamera.Damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, currentCamera.Damping * Time.deltaTime);
	}

	private void Instance_OnLocalPlayerJoined(Player player) {
		localPlayer = player;
		cameraLookTarget = localPlayer.transform.Find("CamerTarget");

		if(cameraLookTarget == null) {
			cameraLookTarget = localPlayer.transform;
		}
	}
}
