using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {


    // Event Handler
    public delegate void OnGameStateChange(GameState state);
    public event OnGameStateChange OnStateChanged;

    private GameState _current;
    public GameState current
    {
        get
        {
            if (_current == null)
            {
                _current = GameState.Pause;
            }
            return _current;
        }
        set
        {
            _current = value;
            if (OnStateChanged != null)
            {
                OnStateChanged(_current);
            }
        }
    }
}
