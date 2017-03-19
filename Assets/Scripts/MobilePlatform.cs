using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour {

    public float speed = 1.0F;

    private Vector3 startMarker;
    private Vector3 endMarker;
    private Vector3 currentMarker;
    private float startTime;
    private float journeyLength;
    private bool tilt = false;

    // Use this for initialization
    void Start () {
        startMarker = this.transform.GetChild(0).position;
        endMarker = this.transform.GetChild(1).position;
        currentMarker = endMarker;
    }

    Vector3 velocity = Vector3.zero;
	// Update is called once per frame
	void FixedUpdate () {
        if (Vector3.Distance(this.transform.position, currentMarker) < 1f) {
            tilt = !tilt;
        }
        if (tilt) {
            currentMarker = startMarker;
        } else {
            currentMarker = endMarker;
        }
        transform.position = Vector3.SmoothDamp(this.transform.position, currentMarker,ref velocity, speed);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            collision.transform.parent = this.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            collision.transform.parent = null;
        }
    }
}
