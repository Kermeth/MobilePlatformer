using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    private Rigidbody2D rigidb {
        get {
            return this.GetComponent<Rigidbody2D>();
        }
    }
    private float minimumForce = 100f;

    #region Monobehaviours
    void OnEnable()
    {
        //Suscribe to movement Events
        GameManager.Instance.input.OnLeftScreenTouchStationary += MoveLeft;
        GameManager.Instance.input.OnRightScreenTouchStationary += MoveRight;
        GameManager.Instance.input.OnJumpTouched += Jump;

        GameManager.Instance.currentState = GameState.Playing;
    }

    void OnDisable() {
        GameManager.Instance.input.OnLeftScreenTouchStationary -= MoveLeft;
        GameManager.Instance.input.OnRightScreenTouchStationary -= MoveRight;
        GameManager.Instance.input.OnJumpTouched -= Jump;
    }

    
    #endregion Monobehaviours

    private void MoveLeft() {
        rigidb.AddForce(Vector2.left *minimumForce);
    }

    private void MoveRight() {
        rigidb.AddForce(Vector2.right * minimumForce);
    }

    private void Jump() {
        rigidb.AddForce(Vector2.up * minimumForce * 50f);
    }

}
