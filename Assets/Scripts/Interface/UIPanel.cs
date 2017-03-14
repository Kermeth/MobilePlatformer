using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIPanel : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}

    public void Show() {
        anim.SetBool("show", true);
    }

    public void Hide() {
        anim.SetBool("show", false);
    }
	
	
}
