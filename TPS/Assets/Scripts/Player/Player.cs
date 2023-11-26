using Framework;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(AudioController))]
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
    private MouseInput mouseControl = new MouseInput();
	[SerializeField]
	AudioController footStepsAudio = null;

	public PlayerAim playerAim;

	private PlayerShoot _mPlayerShoot;
	public PlayerShoot PlayerShoot {
		get {
			if(_mPlayerShoot == null)
				_mPlayerShoot = GetComponent<PlayerShoot>();
			return _mPlayerShoot;
		}
	}

	private CharacterController _mMoveController;
	private CharacterController MoveController {
		get {
			if(_mMoveController == null) {
				_mMoveController = GetComponent<CharacterController>();
			}
			return _mMoveController;
		}
	}

	private InputController _inputController;
    private Vector2 _mouseInput;

	private PlayerStates _mPlayerState;
	public PlayerStates PlayerState {
		get {
			if(_mPlayerState == null) {
				_mPlayerState = GetComponent<PlayerStates>();
			}

			return _mPlayerState;
		}
	}

	public PlayerHealth PlayerHealth {
		get {
			if(_playerHealth == null) {
				PlayerHealth = GetComponent<PlayerHealth>();
			}
			return _playerHealth;
		}
        set {
			if(!Equals(_playerHealth, value)) {
				_playerHealth = value;
			}
		}
	}
	private PlayerHealth _playerHealth;

	// Start is called before the first frame update
    private void Awake() {
		_inputController = GameManager.Instance.InputController;
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

    private void Look() {
		_mouseInput.x = Mathf.Lerp(_mouseInput.x, _inputController.mouseInput.x, 1f / mouseControl.damping.x);
		_mouseInput.y = Mathf.Lerp(_mouseInput.y, _inputController.mouseInput.y, 1f / mouseControl.damping.y);

		transform.Rotate(Vector3.up * (_mouseInput.x * mouseControl.sensetivity.x));

		playerAim.SetRotation(_mouseInput.y * mouseControl.sensetivity.y);
	}

    private void Move() {
		var moveSpeed = settings.WalkSpeed;

		if(_inputController.IsSprinting) {
			moveSpeed = settings.SprintSpeed;
		}

		Vector2 direction = new Vector2(_inputController.vertical * moveSpeed, _inputController.horizontal * moveSpeed);

		if(direction != Vector2.zero) {
			footStepsAudio.Play();
		}

        var transform1 = transform;
        MoveController.SimpleMove(transform1.forward * (direction.x * settings.SpeedFactor) + transform1.right * (direction.y * settings.SpeedFactor));
		//MoveController.Move(direction);

	}
}
