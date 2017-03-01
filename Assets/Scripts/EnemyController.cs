using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Rigidbody2D rb {
        private set; get;
    }
    public Collider2D floor {
        private set; get;
    }

    private IState currentState;
    private PatrolState patrolState;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();

        patrolState = new PatrolState(this);
        ChangeState(patrolState);
    }
	
    void FixedUpdate() {
        currentState.OnStateUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (CheckGrounded(collision.gameObject)) {
            this.floor = collision.collider;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (CheckGrounded(collision.gameObject)) {
            this.floor = null;
        }
    }



	public void ChangeState(IState newState) {
        if (currentState != null) currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }

    private bool CheckGrounded(GameObject subject) {
        if (subject.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            return true;
        } else {
            return false;
        }
    }
}
