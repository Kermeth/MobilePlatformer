using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

    public float healAmount = 5f;

	void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collider.gameObject.GetComponent<Stats>().Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
