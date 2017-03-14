using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MainMenuController : MonoBehaviour {

    public int currentState = 0;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
    public void ChangeState(int newState) {
        currentState = newState;
        anim.SetInteger("State", newState);
    }
	
}
