using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public bool active;

    private Animator anim;

    void Start() {
        anim = this.GetComponentInChildren<Animator>();
    }

    void Update() {
        anim.SetBool("active", active);
    }

    public void ActivateCheckPoint() {
        this.active = true;
        GameManager.Instance.ActivateCheckPoint(this);
    }
	
}
