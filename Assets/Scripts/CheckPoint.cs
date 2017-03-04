using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public bool active;

    public void ActivateCheckPoint() {
        this.active = true;
        GameManager.Instance.ActivateCheckPoint(this);
    }
	
}
