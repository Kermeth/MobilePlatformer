using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    void Start() {
        if (GameManager.Instance.firstTime && GameManager.Instance.GetCurrentScene().buildIndex==0) {
            GameManager.Instance.firstTime = false;
            GameManager.Instance.GoToScene("MainMenu");
        }
    }

    public void GoToScene(string scene) {
        if (GameManager.Instance.firstTime) {
            GameManager.Instance.firstTime = false;
        }
        GameManager.Instance.GoToScene(scene);
    }

	public void FinishLevel() {

    }

}
