using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {

    #region Constructor
    protected GameManager() {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        this.FillCheckPoints();
    }
    #endregion

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
    public void PlayerRespawn() {
        player.transform.position = GetActiveCheckPoint().transform.position;
        player.GetComponent<Stats>().Heal(player.GetComponent<Stats>().maxLife);
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

    #region CheckPoints
    List<CheckPoint> checkPoints = new List<CheckPoint>();
    public void FillCheckPoints() {
        checkPoints.Clear();
        checkPoints.AddRange(FindObjectsOfType<CheckPoint>());
    }

    public CheckPoint GetActiveCheckPoint() {
        return checkPoints.FirstOrDefault<CheckPoint>(x => x.active == true);
    }

    public void ActivateCheckPoint(CheckPoint checkpoint) {
        foreach (CheckPoint check in checkPoints.Where<CheckPoint>(x=> !x.Equals(checkpoint))) {
            check.active = false;
        }
    }
    #endregion

}
