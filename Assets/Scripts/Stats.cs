using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float maxLife = 10f;
    public float currentLife {
        get; private set;
    }

    void Start() {
        currentLife = maxLife;
    }

    public void GetHurt(float amount) {
        currentLife -= amount;
        if (currentLife <= 0f) {
            currentLife = 0f;
            Die();
        }
    }

    public void Heal(float amount) {
        currentLife += amount;
        if (currentLife > maxLife) {
            currentLife = maxLife;
        }
    }

    private void Die() {
        if (this.GetComponent<Player>()) {
            GameManager.Instance.PlayerRespawn();
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
