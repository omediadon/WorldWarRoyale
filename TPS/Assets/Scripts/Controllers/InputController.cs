using UnityEngine;

public class InputController: MonoBehaviour {
	public float vertical;
	public float horizontal;
	public Vector2 mouseInput;
	public bool fire1;
	public bool reload;
	public bool isSprinting;
	public bool isCrouching;


	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		fire1 = Input.GetButton("Fire1");
		reload = Input.GetKey(KeyCode.R);
		isSprinting = Input.GetKey(KeyCode.LeftShift);
		isCrouching = Input.GetKey(KeyCode.C);
	}
}
