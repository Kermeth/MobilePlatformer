using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;

    public Rigidbody2D rb {
        private set; get;
    }
    public Collider2D floor {
        private set; get;
    }
    public Scanner scanner {
        private set; get;
    }
    [HideInInspector]
    public Face facing = Face.RIGHT;

    private Animator anim;

    private IState currentState;
    private ChaseState chaseState;
    private PatrolState patrolState;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        scanner = this.GetComponentInChildren<Scanner>();

        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        ChangeState(patrolState);
    }
	
    void FixedUpdate() {
        if (scanner.target != null) {
            ChangeState(chaseState);
        } else {
            ChangeState(patrolState);
        }

        currentState.OnStateUpdate();

        UpdateAnimations();
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

    private void UpdateAnimations() {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    public void MoveLeft() {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        SetFace(Face.LEFT);
    }

    public void MoveRight() {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        SetFace(Face.RIGHT);
    }

    private void SetFace(Face facing) {
        switch (facing) {
            case Face.LEFT:
                if (this.transform.localScale.x > 0) {
                    this.transform.localScale = new Vector3(
                        -1 * this.transform.localScale.x,
                        this.transform.localScale.y,
                        this.transform.localScale.z);
                }
                break;
            case Face.RIGHT:
                if (this.transform.localScale.x < 0) {
                    this.transform.localScale = new Vector3(
                        -1 * this.transform.localScale.x,
                        this.transform.localScale.y,
                        this.transform.localScale.z);
                }
                break;
        }
        this.facing = facing;
    }

    public void ChangeState(IState newState) {
        if(currentState != newState) {
            if(currentState!=null)currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }
    }

    private bool CheckGrounded(GameObject subject) {
        if (subject.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            return true;
        } else {
            return false;
        }
    }
}
