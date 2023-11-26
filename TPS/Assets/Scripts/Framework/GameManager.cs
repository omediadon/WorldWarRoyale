using System;
using UnityEngine;

namespace Framework
{
    public class GameManager {
        private GameObject _gameObject;

        public event Action<Player> OnLocalPlayerJoined;
        public event Action<Player> OnLocalPlayerJoinedX;
        public event Action<Player> OnLocalPlayerJoinedY;

        private static GameManager _mInstance;
        public static GameManager Instance {
            get {
                if(_mInstance == null) {
                    _mInstance = new GameManager
                    {
                        _gameObject = new GameObject("_gameManager")
                    };
                    _mInstance._gameObject.AddComponent<InputController>();
                    _mInstance._gameObject.AddComponent<Timer>();
                    _mInstance._gameObject.AddComponent<Respawner>();
                }

                return _mInstance;
            }
        }

        private  InputController _mInputController;
        public InputController InputController {
            get {
                if(_mInputController == null) {
                    _mInputController = _gameObject.GetComponent<InputController>();
                }
                return _mInputController;
            }
        }

        private Player _mLocalPlayer;
        public Player LocalPlayer {
            get => _mLocalPlayer;
            set {
                _mLocalPlayer = value;
                OnLocalPlayerJoined?.Invoke(_mLocalPlayer);
                OnLocalPlayerJoinedX?.Invoke(_mLocalPlayer);
                OnLocalPlayerJoinedY?.Invoke(_mLocalPlayer);
            }
        }

        private Timer _mTimer;
        public Timer Timer {
            get {
                if(_mTimer == null) {
                    _mTimer = _gameObject.GetComponent<Timer>();
                }
                return _mTimer;
            }
        }

        private Respawner _mRespawner;
        public Respawner Respawner {
            get {
                if(_mRespawner == null) {
                    _mRespawner = _gameObject.GetComponent<Respawner>();
                }
                return _mRespawner;
            }
        }


        public EventBus EventBus {
            get {
                if(_eventBus == null) {
                    _eventBus = new EventBus();
                }
                return _eventBus;
            }
        }
        private EventBus _eventBus;

    }
}
