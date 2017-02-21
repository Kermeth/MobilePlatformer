using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public float jumpForce = 10f;
    public float moveForce = 10f;

    [SerializeField]
    private bool grounded;

    private Rigidbody2D rigidb;
    private Animator anim;

    private enum Face { RIGHT,LEFT }

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
        SetFace(Face.LEFT);
    }

    private void MoveRight() {
        rigidb.velocity = new Vector2(moveForce, rigidb.velocity.y);
        SetFace(Face.RIGHT);
    }

    private void Jump() {
        if(grounded)
            rigidb.velocity = new Vector2(rigidb.velocity.x, jumpForce);
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
                if(this.transform.localScale.x < 0) {
                    this.transform.localScale = new Vector3(
                        -1 * this.transform.localScale.x,
                        this.transform.localScale.y,
                        this.transform.localScale.z);
                }
                break;
        }
    }

    private void UpdateAnimations() {
        if (anim) {
            anim.SetFloat("speed", Mathf.Abs(rigidb.velocity.x));
            anim.SetBool("grounded", this.grounded);
            anim.SetFloat("fallingSpeed", rigidb.velocity.y);
        }
    }

}
