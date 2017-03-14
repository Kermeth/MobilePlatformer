using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadLevels : MonoBehaviour {

    public GameObject LevelButtonPrefab;
	
    void Start() {
        foreach(Level lvl in GameManager.Instance.GetLevelList()) {
            GameObject button = Instantiate(LevelButtonPrefab);
            button.transform.SetParent(this.transform);
            button.transform.localScale = Vector3.one;
            button.GetComponentInChildren<Text>().text = lvl.name;
            button.GetComponentInChildren<Button>().onClick.AddListener(() => Utils.GoToScene(lvl.sceneIndexName));
        }
    }

}
