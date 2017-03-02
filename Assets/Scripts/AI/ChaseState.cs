using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState {

    EnemyController self;

    public ChaseState(EnemyController self) {
        this.self = self;
    }

    public void OnStateEnter() {
        Debug.Log("Found you!");
        
    }

    public void OnStateExit() {
        Debug.Log("I lost you");
    }

    public void OnStateUpdate() {
        if (self.scanner.target!=null) {
            if(self.DistanceToEnemy() > 3f) {
                if (self.scanner.target.position.x < self.transform.position.x) {
                    self.MoveLeft();
                } else {
                    self.MoveRight();
                }
            } else {
                if (self.CanAttack()) {
                    self.Attack();
                }
            }
        } else {
            self.ChangeState(new PatrolState(self));
        }
    }

    
}
