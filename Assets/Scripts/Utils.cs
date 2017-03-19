using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    void Start() {
        if (GameManager.Instance.firstTime && GameManager.Instance.GetCurrentScene().buildIndex==0) {
            GameManager.Instance.firstTime = false;
            GameManager.Instance.GoToScene("MainMenu");
        }
        //SaveALevel();
    }

    public static void GoToScene(string scene) {
        if (GameManager.Instance.firstTime) {
            GameManager.Instance.firstTime = false;
        }
        GameManager.Instance.GoToScene(scene);
    }

	public void FinishLevel() {
        GoToScene("MainMenu");
    }

    public void SaveALevel() {
        Level level1 = new Level();
        level1.blocked = false;
        level1.name = "Level1";
        LevelsXml newLevels = new LevelsXml();
        newLevels.levelsList.Add(level1);
        newLevels.Save(Application.persistentDataPath + "/levels.xml");
    }

    public void ExitGame() {
        Application.Quit();
    }

}
