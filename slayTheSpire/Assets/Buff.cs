using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff {
  Character originator;
  public string description;
  Action action;


  public Buff(string description,Action action){
    this.description = description;
    this.action = action;
  }

  public virtual void SetOriginator (Character character){
    this.originator = character;
  }

    public void ExecuteBuff(Character player){
      // foreach(Action action in this.actions){
        // Debug.Log(action.effect);
      action.ExecuteAction(player);
      // }
  }
   public virtual void SubcribeToEndTurn(){
    // GameManager.turnEnded += ReduceDuration;
  }
}

public class TemporaryBuff : Buff {
  int duration;

  public TemporaryBuff(string description,Action action,int duration):
    base(description,action){
      this.duration = duration;
    }

  public void ReduceDuration(object sender, EventArgs e){
    duration -= 1;
    Debug.Log(duration+"ASDASD");
  }

  public override void SubcribeToEndTurn(){
    Debug.Log("subscribed to endturn");
    EventManager.afterEnemyTurn += ReduceDuration;
  }

}

