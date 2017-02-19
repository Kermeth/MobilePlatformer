using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    [SerializeField]
    private bool grounded;

    private Rigidbody2D rigidb;
    private Animator anim;
    private float jumpForce = 200f;
    private float moveForce = 10f;


    #region Monobehaviours
    void OnEnable() {
        //Suscribe to movement Events
        SuscribeToEvents();

        GameManager.Instance.currentState = GameState.Playing;
        rigidb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
    }

    void OnDisable() {
        UnSuscribeToEvents();
    }

    void FixedUpdate() {
        UpdateAnimations();
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if (CheckGrounded(collision.gameObject)) {
            grounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if(CheckGrounded(collision.gameObject) && grounded) {
            grounded = false;
        }
    }

    #endregion Monobehaviours

    private void SuscribeToEvents() {
        GameManager.Instance.input.OnLeftScreenTouchStationary += MoveLeft;
        GameManager.Instance.input.OnRightScreenTouchStationary += MoveRight;
        GameManager.Instance.input.OnJumpTouched += Jump;
    }

    private void UnSuscribeToEvents() {
        GameManager.Instance.input.OnLeftScreenTouchStationary -= MoveLeft;
        GameManager.Instance.input.OnRightScreenTouchStationary -= MoveRight;
        GameManager.Instance.input.OnJumpTouched -= Jump;
    }


    private bool CheckGrounded(GameObject subject) {
        if (subject.gameObject.layer==LayerMask.NameToLayer("Ground")) {
            return true;
        } else {
            return false;
        }
    }

    private void MoveLeft() {
        rigidb.velocity = new Vector2(-moveForce, rigidb.velocity.y);
    }

    private void MoveRight() {
        rigidb.velocity = new Vector2(moveForce, rigidb.velocity.y);
    }

    private void Jump() {
        if(grounded)rigidb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateAnimations() {
        if (anim) {
            anim.SetFloat("speedX", Mathf.Abs(rigidb.velocity.x));
        }
    }

}
