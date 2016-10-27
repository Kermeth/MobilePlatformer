using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public Vector3 offset = new Vector3(0,0,-10);
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
            this.transform.position = Vector3.SmoothDamp(this.transform.position, target.transform.position+offset, ref velocity, Time.deltaTime *5f);
        }
    }

}
