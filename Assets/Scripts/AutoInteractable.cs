using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class AutoInteractable : MonoBehaviour {

    public UnityEvent OnInteract;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<Player>()) {
            OnInteract.Invoke();
        }
    }
}
