using UnityEngine;

public class CrossHair : MonoBehaviour
{
	[SerializeField]
	Texture2D texture;
	[SerializeField]
	int size;


	private void OnGUI()
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		GUI.DrawTexture(new Rect(screenPosition.x - size / 2, screenPosition.y - size / 2, size, size), texture);
	}
}
