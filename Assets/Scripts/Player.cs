using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    void OnEnable()
    {
        GameManager.Instance.currentState = GameState.Playing;
    }

}
