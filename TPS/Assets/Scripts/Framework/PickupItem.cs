using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{


	private void OnTriggerEnter(Collider other) {
		if(other.tag != "Player") {
			return;
		}

		PickUp(other.transform);
	}

	public virtual void OnPickUpItem(Transform item) {
	}

	void PickUp(Transform item) {
		OnPickUpItem(item);
	}
}
