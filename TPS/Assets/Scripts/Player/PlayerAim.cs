using UnityEngine;

public class PlayerAim : MonoBehaviour {
	[Range(-25f, -5f)]
	[SerializeField]
	float minAngle = -25f;

	[Range(5f, 25f)]
	[SerializeField]
	float maxAngle = 25f;


	public void SetRotation(float amount) {
		float newAngle = CheckAngle(transform.eulerAngles.x - amount);
		float clampedAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);
		transform.eulerAngles = new Vector3(clampedAngle, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	public float GetAngle() {
		return CheckAngle(transform.eulerAngles.x);
	}

	public float CheckAngle(float value) {
		float angle = value - 180;

		if(angle > 0) {
			return angle - 180;
		}
		return angle + 180;
	}
}
