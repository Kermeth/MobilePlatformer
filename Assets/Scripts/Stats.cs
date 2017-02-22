using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float maxLife = 10f;
    [SerializeField]
    private float currentLife;

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
