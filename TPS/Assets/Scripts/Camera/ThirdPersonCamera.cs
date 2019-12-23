using UnityEngine;

public class ThirdPersonCamera: MonoBehaviour {
	PlayerController localPlayer;
	Transform cameraLookTarget;
	[SerializeField]
	Vector3 cameraOffset;
	[SerializeField]
	float damping;

	// Start is called before the first frame update
	void Awake() {
		GameManager.Instance.OnLocalPlayerJoined += this.Instance_OnLocalPlayerJoined;
	}

	// Update is called once per frame
	void Update() {
		Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z
			+ localPlayer.transform.up * cameraOffset.y
			+ localPlayer.transform.right * cameraOffset.x;

		Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

		transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
	}

	private void Instance_OnLocalPlayerJoined(PlayerController player) {
		localPlayer = player;
		cameraLookTarget = localPlayer.transform.Find("CameraLookTarget");

		if(cameraLookTarget == null) {
			cameraLookTarget = localPlayer.transform;
		}
	}
}
