using UnityEngine;

public class MoveController : MonoBehaviour {
	public void move(Vector2 direction) {
		transform.position += transform.forward * direction.x * Time.deltaTime
			+ transform.right * direction.y * Time.deltaTime;
	}
}
