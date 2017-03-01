using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState {

    EnemyController self;

    public PatrolState(EnemyController self) {
        this.self = self;
    }

    public void OnStateEnter() {
        Debug.Log("I'm Patrolling");
    }

    public void OnStateExit() {
        throw new NotImplementedException();
    }

    public void OnStateUpdate() {
        if (this.self.floor != null) {
            this.self.rb.velocity = Vector2.right;
        }
        
    }

}
