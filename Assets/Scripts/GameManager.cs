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
}
