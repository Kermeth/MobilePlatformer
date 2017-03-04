using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LoadingBar : MonoBehaviour {

    Slider loadingBar;

	// Use this for initialization
	void Start () {
        loadingBar = this.GetComponent<Slider>();
	}
	
	public void UpdateValue(float value) {
        loadingBar.value = value;
    }
}
