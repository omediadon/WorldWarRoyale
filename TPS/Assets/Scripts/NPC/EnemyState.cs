using System;

using UnityEngine;

public class EnemyState : MonoBehaviour {
	public enum EMode {
		AWARE,
		UNAWARE
	}


	public EMode CurrentMode {
		get {
			return _CurrentMode;
		}
		set {
			if(_CurrentMode != value) {
				_CurrentMode = value;
				OnModeChanged?.Invoke(_CurrentMode);
			}
		}
	}
	private EMode _CurrentMode;


	public event Action<EMode> OnModeChanged;

	private void Start() {
		CurrentMode = EMode.UNAWARE;
	}

}
