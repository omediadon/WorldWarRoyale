using UnityEngine;

public class InputController : MonoBehaviour {
	public float vertical;
	public float horizontal;
	public Vector2 mouseInput;
	public bool Fire1;
	public bool Fire2;
	public bool Reload;
	public bool IsSprinting;
	public bool IsCrouching;
	public bool MouseWheelUp;
	public bool MouseWheelDown;

	void Update() {
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		Fire1 = Input.GetButton("Fire1");
		Fire2 = Input.GetButton("Fire2");
		Reload = Input.GetKey(KeyCode.R);
		IsSprinting = Input.GetKey(KeyCode.LeftShift);
		IsCrouching = Input.GetKey(KeyCode.C);
		MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0f;
		MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0f;
	}
}
