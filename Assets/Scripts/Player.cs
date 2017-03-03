using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public float jumpForce = 10f;
    public float moveForce = 10f;
    public float damage = 1f;
    public float specialAttackDamage = 2f;
    public int specialAttacComboNumber = 4;
    public float attackCooldown = 0.33f;
    public float specialAttackCooldown = 2.33f;

    [SerializeField]
    private bool grounded;

    private Rigidbody2D rigidb;
    private Animator anim;
    private int comboCount = 0;
    public float currentAttackCooldown { private set; get; }

    #region Monobehaviours
    void OnEnable() {
        //Suscribe to movement Events
        SuscribeToEvents();

        GameManager.Instance.currentState = GameState.Playing;
        rigidb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        currentAttackCooldown = 0f;
    }

    void OnDisable() {
        UnSuscribeToEvents();
    }

    void FixedUpdate() {
        UpdateAnimations();
        currentAttackCooldown -= Time.fixedDeltaTime;
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

    public int GetCurrentComboCount() {
        return this.comboCount;
    }

    private void SuscribeToEvents() {
        GameManager.Instance.input.OnLeftScreenTouchStationary += MoveLeft;
        GameManager.Instance.input.OnRightScreenTouchStationary += MoveRight;
        GameManager.Instance.input.OnJumpTouched += Jump;
        GameManager.Instance.input.OnAttackTouched += Attack;
    }

    private void UnSuscribeToEvents() {
        GameManager.Instance.input.OnLeftScreenTouchStationary -= MoveLeft;
        GameManager.Instance.input.OnRightScreenTouchStationary -= MoveRight;
        GameManager.Instance.input.OnJumpTouched -= Jump;
        GameManager.Instance.input.OnAttackTouched += Attack;
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

    private void Attack() {
        if (currentAttackCooldown <= 0f) {
            
            StartCoroutine(CooldownAttack(comboCount++));
        }
    }

    private IEnumerator CooldownAttack(int comboCount) {
        if (comboCount < specialAttacComboNumber) {
            currentAttackCooldown = attackCooldown;
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(0.1f);
            PerformAttack(damage);
        } else {
            currentAttackCooldown = specialAttackCooldown;
            this.comboCount = 0;
            anim.SetTrigger("special");
            yield return new WaitForSeconds(0.1f);
            PerformAttack(specialAttackDamage);
        }
        yield return new WaitForEndOfFrame();
    }

    private void PerformAttack(float dmg) {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position + this.transform.forward * 3f, 3f, this.rigidb.transform.forward);
        foreach (RaycastHit2D hit in hits) {
            if ((hit.collider.gameObject != this.gameObject) && hit.collider.GetComponent<Stats>() != null) {
                //if hit is diferent from ourselfs and can be damaged
                hit.collider.GetComponent<Stats>().GetHurt(dmg);
            }
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
