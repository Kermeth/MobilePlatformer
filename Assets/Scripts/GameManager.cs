using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {


    #region GameState
    // Event Handler
    public delegate void OnGameStateChange(GameState state);
    public event OnGameStateChange OnStateChanged;
    private GameState _currentState;
    public GameState currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
            if (OnStateChanged != null)
            {
                OnStateChanged(_currentState);
            }
        }
    }
    #endregion GameState

    #region Player
    private Player _player;
    public Player player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
            }
            return _player;
        }
    }
    #endregion Player

    #region InputManager
    private InputManager _input;
    public InputManager input {
        get {
            if (_input == null) {
                _input=FindObjectOfType<InputManager>();
                if(_input == null) {
                    GameObject g = new GameObject("InputManager(Singleton)");
                    _input = g.AddComponent<InputManager>();
                }
            }
            return _input;
        }
    }
    #endregion InputManager
}
