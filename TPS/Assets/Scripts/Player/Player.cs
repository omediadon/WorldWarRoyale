using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStates))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour {
	[System.Serializable]
	public class MouseInput {
		public bool lockMouse = false;
		public Vector2 damping = Vector2.zero;
		public Vector2 sensetivity = Vector2.one;
	}

	[SerializeField]
	SoldierPro settings = null;
	[Header("Mouse controls")]
	[SerializeField]
	MouseInput mouseControl = new MouseInput();
	[SerializeField]
	AudioController footStepsAudio = null;

	public PlayerAim playerAim;

	private PlayerShoot m_PlayerShoot;
	public PlayerShoot PlayerShoot {
		get {
			if(m_PlayerShoot == null)
				m_PlayerShoot = GetComponent<PlayerShoot>();
			return m_PlayerShoot;
		}
	}

	private CharacterController m_MoveController;
	private CharacterController MoveController {
		get {
			if(m_MoveController == null) {
				m_MoveController = GetComponent<CharacterController>();
			}
			return m_MoveController;
		}
	}

	private InputController inputController;
	Vector2 mouseInput;

	private PlayerStates m_PlayerState;
	public PlayerStates PlayerState {
		get {
			if(m_PlayerState == null) {
				m_PlayerState = GetComponent<PlayerStates>();
			}

			return m_PlayerState;
		}
	}




	public PlayerHealth PlayerHealth {
		get {
			if(_PlayerHealth == null) {
				PlayerHealth = GetComponent<PlayerHealth>();
			}
			return _PlayerHealth;
		}
		set {
			if(_PlayerHealth != value) {
				_PlayerHealth = value;
			}
		}
	}
	private PlayerHealth _PlayerHealth;



	// Start is called before the first frame update
	void Awake() {
		inputController = GameManager.Instance.InputController;
		GameManager.Instance.LocalPlayer = this;
		if(mouseControl.lockMouse) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Update is called once per frame
	void Update() {
		if(!PlayerHealth.IsAlive) {
			return;
		}

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
		float moveSpeed = settings.WalkSpeed;

		if(inputController.IsSprinting) {
			moveSpeed = settings.SprintSpeed;
		}

		Vector2 direction = new Vector2(inputController.vertical * moveSpeed, inputController.horizontal * moveSpeed);

		if(direction != Vector2.zero) {
			footStepsAudio.Play();
		}

		MoveController.SimpleMove(transform.forward * direction.x * settings.SpeedFactor + transform.right * direction.y * settings.SpeedFactor);
		//MoveController.Move(direction);

	}
}
