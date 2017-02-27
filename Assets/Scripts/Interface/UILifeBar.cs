using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class UILifeBar : MonoBehaviour {

    private Scrollbar scrollBar;
    /// <summary>
    /// The Stats to read the current and max life
    /// </summary>
    private Stats stats;

	// Use this for initialization
	void Start () {
        scrollBar = this.GetComponent<Scrollbar>();
        stats = this.GetComponentInParent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
        if (stats != null) {
            scrollBar.size = (stats.currentLife / stats.maxLife);
        }
	}
}
