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
            Die();
        }
    }

    private void Die() {
        this.gameObject.SetActive(false);
    }
}
