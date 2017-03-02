using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Scanner : MonoBehaviour {

    public Transform target {
        private set; get;
    }

	void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            target = collider.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        Debug.Log(collider.name);
        if(collider.transform == target) {
            target = null;
        }
    }
}
