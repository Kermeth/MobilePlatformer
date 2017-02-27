using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyCollider : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Stats>()) {
            collision.gameObject.GetComponent<Stats>().GetHurt(collision.gameObject.GetComponent<Stats>().maxLife);
        }
    }
}
