using UnityEngine;

public class CrossHair : MonoBehaviour {
	[SerializeField]
	Texture2D texture = null;
	[SerializeField]
	int size = 32;


	private void OnGUI() {
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		if (texture != null) {
			GUI.DrawTexture(new Rect(screenPosition.x - size / 2, screenPosition.y - size / 2, size, size), texture);
		}
	}
}
