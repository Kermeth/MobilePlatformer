using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    public float attackDmg = 1f;
    public float attackCooldown = 3f;

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
    private float currentAttackCooldown;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        scanner = this.GetComponentInChildren<Scanner>();

        currentAttackCooldown = attackCooldown;

        ChangeState(new PatrolState(this));
    }
	
    void FixedUpdate() {
        currentAttackCooldown -= Time.fixedDeltaTime;

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

    public bool CanAttack() {
        if (currentAttackCooldown <= 0f) {
            return true;
        } else {
            return false;
        }
    }

    public void Attack() {
        if (CanAttack()) {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack() {
        yield return new WaitForEndOfFrame();
        currentAttackCooldown = attackCooldown;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.1f);//let the animation start
        if (DistanceToEnemy() < 3f) {
            scanner.target.GetComponent<Stats>().GetHurt(this.attackDmg);
        }
    }

    public float DistanceToEnemy() {
        if (scanner.target != null) {
            return Vector2.Distance(this.scanner.target.position, this.transform.position);
        } else {
            return Mathf.Infinity;
        }
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
