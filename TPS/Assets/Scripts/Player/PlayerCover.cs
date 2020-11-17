using UnityEngine;

public class PlayerCover : MonoBehaviour {
	bool canTakeCover;
	bool isInCover;
	RaycastHit closestHit = new RaycastHit();


	[SerializeField]
	int numberOfRays = 8;

	[SerializeField]
	LayerMask coverMask = new LayerMask();


	public void SetPlayerCoverAllowed(bool value) {
		canTakeCover = value;
	}

	private void Update() {
		if(!canTakeCover) {
			return;
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			FindCover();
			if(closestHit.distance == 0) {
				return;
			}

			GameManager.Instance.EventBus.RaiseEvent("CoverToggle"); 
			transform.rotation = Quaternion.LookRotation(closestHit.normal) * Quaternion.Euler(0, 180, 0);

		}
	}

	private void FindCover() {
		closestHit = new RaycastHit();
		float angleStep = 360 / numberOfRays;
		for(int i = 0; i < angleStep; i++) {
			Quaternion angle = Quaternion.AngleAxis(i * angleStep, transform.up);
			CheckClosestPoint(angle);
		}
		Debug.DrawLine(transform.position + Vector3.up * .3f, closestHit.point, Color.blue, 2);
	}

	private void CheckClosestPoint(Quaternion angle) {
		Debug.DrawRay(transform.position + Vector3.up * .3f, angle * Vector3.forward * 5);

		RaycastHit hit;
		if(Physics.Raycast(transform.position + Vector3.up * .3f, angle * Vector3.forward, out hit, 5, coverMask)) {
			if(closestHit.distance == 0 || closestHit.distance > hit.distance) {
				closestHit = hit;
			}
			Debug.DrawLine(transform.position + Vector3.up * .3f, hit.point, Color.magenta, 1);
		}
	}
}
