using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public bool readPlayerSpeed;

    public float basicSpeed;
    public float playerSpeedFactor;

    private Vector2 savedOffset;
    private Renderer render;

	// Use this for initialization
	void Start () {
        render = this.GetComponent<Renderer>();
        savedOffset = render.sharedMaterial.GetTextureOffset("_MainTex");
	}

    void Update() {
        Vector2 currentOffset=render.sharedMaterial.GetTextureOffset("_MainTex");
        Vector2 newOffset = Vector2.zero;
        if (readPlayerSpeed) {
            Vector2 playerSpeed = GameManager.Instance.player.GetComponent<Rigidbody2D>().velocity;
            newOffset = new Vector2(currentOffset.x + basicSpeed*0.01f + (playerSpeed.x*0.001f*playerSpeedFactor), currentOffset.y);
        } else {
            newOffset = new Vector2(currentOffset.x + basicSpeed, currentOffset.y);
        }
        render.sharedMaterial.SetTextureOffset("_MainTex", newOffset);
    }

    void OnDisable() {
        if(render!= null) {
            render.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
        }
    }
}
