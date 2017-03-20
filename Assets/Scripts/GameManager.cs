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

    #region SceneManagement
    public Scene GetCurrentScene() {
        return SceneManager.GetActiveScene();
    }

    public void GoToScene(string name) {
        StartCoroutine(loadingOperation(name));
    }
    public void GoToScene(int buildIndex) {
        
    }
    public bool firstTime = true;
    private IEnumerator loadingOperation(string sceneToGo) {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("LoadingScreen");
        yield return new WaitForEndOfFrame();
        LoadingBar loadingBar = FindObjectOfType<LoadingBar>();
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneToGo);
        while (!loadingOperation.isDone) {
            loadingBar.UpdateValue(loadingOperation.progress);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    private void SceneManager_sceneLoaded(Scene currentScene, LoadSceneMode sceneMode) {
        this.FillCheckPoints();
    }
    #endregion

    #region UIPanels
    private List<UIPanel> _uiPanels = new List<UIPanel>();
    public UIPanel FindPanelContains(string name) {
        _uiPanels.Clear();
        _uiPanels.AddRange(FindObjectsOfType<UIPanel>());
        foreach(UIPanel panel in _uiPanels) {
            if (panel.name.Contains(name))
                return panel;
        }
        return null;
    }
    #endregion

    #region LevelsXml
    private LevelsXml _levelsList;
    public LevelsXml LevelsList {
        get {
            if (_levelsList == null) {
                TextAsset xml = (TextAsset)Resources.Load("levels");
                _levelsList = LevelsXml.LoadFromText(xml.text);
            }
            return _levelsList;
        }
    }
    public List<Level> GetLevelList() {
        return LevelsList.levelsList;
    }
    public void UnlockNextLevel(string currentLevel) {
        int index=LevelsList.levelsList.FindIndex( x => x.sceneIndexName.Equals(currentLevel));
        Level nextLevel=LevelsList.levelsList.ElementAt(index + 1);
        if (nextLevel != null) {
            nextLevel.blocked = false;
        }
        
    }
    #endregion

}
