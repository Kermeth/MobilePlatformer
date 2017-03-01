using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState {

    EnemyController self;
    float minimumX;
    float maximumX;

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
            minimumX = this.self.floor.bounds.min.x;
            maximumX = this.self.floor.bounds.max.x;
        }

        if (this.self.facing == Face.RIGHT) {
            if(this.self.transform.position.x < maximumX) {
                self.MoveRight();
            } else {
                self.MoveLeft();
            }
        } else {
            if(this.self.transform.position.x > minimumX) {
                self.MoveLeft();
            } else {
                self.MoveRight();
            }
        }
    }

}
