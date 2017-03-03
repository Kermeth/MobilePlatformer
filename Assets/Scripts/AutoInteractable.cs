using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoInteractable : MonoBehaviour {

    public UnityEvent OnInteract;

    void OnTriggerEnter2D(Collider2D collider) {
        OnInteract.Invoke();
    }
}
