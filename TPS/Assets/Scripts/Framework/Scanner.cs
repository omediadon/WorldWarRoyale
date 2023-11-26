using System;
using System.Collections.Generic;
using Framework;
using Framework.Extensions;

using UnityEngine;

public class Scanner : MonoBehaviour {
	[Range(0.2f, 2f)]
	[SerializeField]
	float scanSpeed = 0.2f;
	[Range(0.01f, 50.0f)]
	public float ScanRange = 5;
	[Range(70.0f, 110.0f)]
	[SerializeField]
	float fieldOfView = 90f;
	[SerializeField]
	LayerMask mask = new LayerMask();


	public event Action OnScanReady;

	private void Start() {
		PrepareScan();
	}


	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		DrawCircle();

		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(this.fieldOfView / 2) * ScanRange);
		Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * ScanRange);

	}

	private void DrawCircle() {
		float theta = 0;
		float x = ScanRange * Mathf.Cos(theta);
		float y = ScanRange * Mathf.Sin(theta);
		Vector3 pos = transform.position + new Vector3(x, 0, y);
		Vector3 newPos = pos;
		Vector3 lastPos = pos;
		for(theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f) {
			x = ScanRange * Mathf.Cos(theta);
			y = ScanRange * Mathf.Sin(theta);
			newPos = transform.position + new Vector3(x, 0, y);
			Gizmos.DrawLine(pos, newPos);
			pos = newPos;
		}
		Gizmos.DrawLine(pos, lastPos);
	}

	Vector3 GetViewAngle(float angle) {
		float rad = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
	}

	public List<T> ScanForTargets<T>() {
		Collider[] results = Physics.OverlapSphere(transform.position, ScanRange);

		List<T> targets = new List<T>();

		for(int i = 0; i < results.Length; i++) {
			var target = results[i].gameObject.GetComponent<T>();
			if(target == null || !transform.IsInLineOfSight(results[i].transform.position, fieldOfView, mask, Vector3.up)) {
				continue;
			}

			targets.Add(target);
		}

		PrepareScan();

		return targets;
	}

	void PrepareScan() {
		GameManager.Instance.Timer.Add(() => {
			OnScanReady?.Invoke();
		}, scanSpeed);
	}

}
