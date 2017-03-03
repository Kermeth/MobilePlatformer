using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SkillReader : MonoBehaviour {

    private Slider slider;
    private Player playerReference;

	// Use this for initialization
	void Start () {
        slider = this.GetComponent<Slider>();
        playerReference = GameManager.Instance.player;
	}
	
	// Update is called once per frame
	void Update () {
        if( playerReference.GetCurrentComboCount() == 0 ) {
            slider.maxValue = playerReference.specialAttackCooldown;
        } else {
            slider.maxValue = playerReference.attackCooldown;
        }
        slider.value = playerReference.currentAttackCooldown;
	}
}
