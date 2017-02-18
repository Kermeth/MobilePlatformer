using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public float offsetX = 1f;
    public float offsetY = 1f;
    public bool following = true;
    private GameObject target;

    #region Monobehaviours

    void OnEnable()
    {
        FollowPlayer();
    }

    #endregion Monobehaviours

    public void FollowPlayer()
    {
        target = GameManager.Instance.player.gameObject;
    }

    public void FollowGameObject(GameObject target)
    {
        this.target = target;
    }

    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {
        if (GameManager.Instance.currentState==GameState.Playing)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position,
                target.transform.position + new Vector3(
                    target.transform.localScale.x * offsetX,
                    offsetY,
                    -10),
                ref velocity, 
                Time.deltaTime *5f);
        }
    }

}
