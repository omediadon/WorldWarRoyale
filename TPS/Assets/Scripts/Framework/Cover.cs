using UnityEngine;

public class Cover : MonoBehaviour {
	[SerializeField]
	//Collider Trigger = null;

	private void OnTriggerEnter(Collider other) {
		if(!CheckLocalPlayer(other)){
			return; 
		}

		PlayerCover playerCover = GameManager.Instance.LocalPlayer.GetComponent<PlayerCover>();
		playerCover.SetPlayerCoverAllowed(true);
	}

	private void OnTriggerExit(Collider other) {
		if(!CheckLocalPlayer(other)){
			return; 
		}

		PlayerCover playerCover = GameManager.Instance.LocalPlayer.GetComponent<PlayerCover>();
		playerCover.SetPlayerCoverAllowed(false);
	}

	bool CheckLocalPlayer(Collider other) {
		return other.tag == "Player" && other.GetComponent<Player>() == GameManager.Instance.LocalPlayer;
	}
}
