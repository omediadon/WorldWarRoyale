using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStates))]
public class Player : MonoBehaviour {
	[System.Serializable]
	public class MouseInput {
		public bool lockMouse = false;
		public Vector2 damping = Vector2.zero;
		public Vector2 sensetivity = Vector2.one;
	}

	[Range(0.02f, 0.2f)]
	[SerializeField]
	float SpeedFactor = 0.05f;
	[SerializeField]
	float walkSpeed = 4;
	/*
	[SerializeField]
	float crouchSpeed = 3;
	*/
	[SerializeField]
	float sprintSpeed = 5;
	[Header("Mouse controls")]
	[SerializeField]
	MouseInput mouseControl = new MouseInput();
	[SerializeField]
	AudioController footStepsAudio = null;

	public PlayerAim playerAim;

	private PlayerShoot m_PlayerShoot;
	public PlayerShoot PlayerShoot {
		get {
			if (m_PlayerShoot == null)
				m_PlayerShoot = GetComponent<PlayerShoot>();
			return m_PlayerShoot;
		}
	}

	private CharacterController m_MoveController;
	private CharacterController MoveController {
		get {
			if (m_MoveController == null) {
				m_MoveController = GetComponent<CharacterController>();
			}
			return m_MoveController;
		}
	}
	private InputController inputController;
	Vector2 mouseInput;

	/*
	private CrossHair m_CrossHair;
	private CrossHair CrossHair {
		get {
			if(m_CrossHair == null) {
				m_CrossHair = GetComponentInChildren<CrossHair>();
			}

			return m_CrossHair;
		}
	}
	*/

	private PlayerStates m_PlayerState;
	public PlayerStates PlayerState {
		get {
			if (m_PlayerState == null) {
				m_PlayerState = GetComponent<PlayerStates>();
			}

			return m_PlayerState;
		}
	}


	// Start is called before the first frame update
	void Awake() {
		inputController = GameManager.Instance.InputController;
		GameManager.Instance.LocalPlayer = this;
		if (mouseControl.lockMouse) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Update is called once per frame
	void Update() {
		Move();
		Look();
	}

	void Look() {
		mouseInput.x = Mathf.Lerp(mouseInput.x, inputController.mouseInput.x, 1f / mouseControl.damping.x);
		mouseInput.y = Mathf.Lerp(mouseInput.y, inputController.mouseInput.y, 1f / mouseControl.damping.y);

		transform.Rotate(Vector3.up * mouseInput.x * mouseControl.sensetivity.x);

		playerAim.SetRotation(mouseInput.y * mouseControl.sensetivity.y);
	}

	void Move() {
		float moveSpeed = walkSpeed;

		if (inputController.IsSprinting) {
			moveSpeed = sprintSpeed;
		}

		Vector2 direction = new Vector2(inputController.vertical * moveSpeed, inputController.horizontal * moveSpeed);

		if (direction != Vector2.zero) {
			footStepsAudio.Play();
		}

		MoveController.Move(transform.forward * direction.x * SpeedFactor + transform.right * direction.y * SpeedFactor);
		//MoveController.Move(direction);

	}
}
